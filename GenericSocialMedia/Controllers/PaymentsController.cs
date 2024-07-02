using MediatR;
using GenericSocialMedia.Application.Common.DTOs;
using GenericSocialMedia.Application.Features.PaymentFeatures.CreatePayment;
using GenericSocialMedia.Application.Features.PaymentFeatures.CreatePaymentDetails;
using GenericSocialMedia.Application.Features.PaymentFeatures.GetAllPayments;
using GenericSocialMedia.Application.Features.PaymentFeatures.GetSuccesfulPayments;
using GenericSocialMedia.Application.Features.UserFeatures.UpdateUserSubscription;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace GenericSocialMedia.Controllers
{
    [Route("api/payments")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetSuccesfulPaymentsResponse>>> GetAllSuccessful(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetSuccesfulPaymentsRequest(), cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<GetAllPaymentsResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllPaymentsRequest(), cancellationToken);
            return Ok(response);
        }

    }

    [Route("api/create-checkout-session")]
    [ApiController]
    public class CheckoutApiController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public CheckoutApiController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] StripeCheckoutSessionRequest request, CancellationToken cancellationToken)
        {
            var domain = _configuration["FrontendUrl"];
            var priceService = new PriceService();
            var priceOptions = new PriceListOptions
            {
                Product = request.StripeSubscriptionId
            };
            StripeList<Price> prices = priceService.List(priceOptions);

            var options = new SessionCreateOptions
            {
                Locale = "es",
                CustomerEmail = request.Email,
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = prices.Where(x => x.Active).FirstOrDefault().Id,
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = domain + "/subscriptions/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/subscriptions/cancel"
            };
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            await _mediator.Send(new CreatePaymentRequest(request.Email, session.Id, request.StripeSubscriptionId), cancellationToken);
            return new StatusCodeResult(303);
        }
    }

    [Route("create-portal-session")]
    [ApiController]
    public class PortalApiController : Controller
    {
        [HttpPost]
        public ActionResult Create()
        {
            // For demonstration purposes, we're using the Checkout session to retrieve the customer ID.
            // Typically this is stored alongside the authenticated user in your database.
            var checkoutService = new SessionService();
            var checkoutSession = checkoutService.Get(Request.Form["session_id"]);

            // This is the URL to which your customer will return after
            // they are done managing billing in the Customer Portal.
            var returnUrl = "http://localhost:4242";

            var options = new Stripe.BillingPortal.SessionCreateOptions
            {
                Customer = checkoutSession.CustomerId,
                ReturnUrl = returnUrl,
            };
            var service = new Stripe.BillingPortal.SessionService();
            var session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }

    [Route("webhook")]
    [ApiController]
    public class WebhookController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public WebhookController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("stripe")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            // Replace this endpoint secret with your endpoint's unique secret
            // If you are testing with the CLI, find the secret by running 'stripe listen'
            // If you are using an endpoint defined with the API or dashboard, look in your webhook settings
            // at https://dashboard.stripe.com/webhooks
            string endpointSecret = _configuration["Stripe:Secret"];
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];
                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, endpointSecret);
                if (stripeEvent.Type == Events.ChargeFailed)
                {
                    var charge = stripeEvent.Data.Object as Stripe.Charge;
                    Console.WriteLine("A subscription was canceled.", charge.Id);
                    await _mediator.Send(new CreatePaymentDetailsRequest(charge.BillingDetails.Email, charge.Id, "Fail", charge.FailureMessage), cancellationToken);
                }
                if (stripeEvent.Type == Events.ChargeSucceeded)
                {
                    var charge = stripeEvent.Data.Object as Stripe.Charge;
                    Console.WriteLine("A subscription was canceled.", charge.Id);
                    var paymentResponse = await _mediator.Send(new CreatePaymentDetailsRequest(charge.BillingDetails.Email, charge.Id, "Success", "Charge succeeded"), cancellationToken);
                    var response = await _mediator.Send(new UpdateUserSubscriptionRequest(paymentResponse.PaymentId), cancellationToken);
                }
                else if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var paymentIntent = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    Console.WriteLine("A subscription was updated.", paymentIntent.Id);
                }
                else if (stripeEvent.Type == Events.PaymentIntentCreated)
                {
                    var paymentIntent = stripeEvent.Data.Object as Stripe.PaymentIntent;
                    Console.WriteLine("A subscription was created.", paymentIntent.Id);
                    // Then define and call a method to handle the successful payment intent.
                    // handleSubscriptionUpdated(subscription);
                }
                else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    var paymentIntent = stripeEvent.Data.Object as Stripe.PaymentIntent;
                    Console.WriteLine("A subscription was created.", paymentIntent.Id);
                    // Then define and call a method to handle the successful payment intent.
                    // handleSubscriptionUpdated(subscription);
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return BadRequest();
            }
        }
    }
}
