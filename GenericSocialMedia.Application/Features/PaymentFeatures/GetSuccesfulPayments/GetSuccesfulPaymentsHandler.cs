using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.GetSuccesfulPayments
{
    public class GetSuccesfulPaymentsHandler : IRequestHandler<GetSuccesfulPaymentsRequest, List<GetSuccesfulPaymentsResponse>>
    {
        private readonly IPaymentDetailsRepository _paymentDetailsRepository;
        private readonly IMapper _mapper;
        public GetSuccesfulPaymentsHandler(IPaymentDetailsRepository paymentDetailsRepository, IMapper mapper)
        {
            _paymentDetailsRepository = paymentDetailsRepository;
            _mapper = mapper;
        }
        public async Task<List<GetSuccesfulPaymentsResponse>> Handle(GetSuccesfulPaymentsRequest request, CancellationToken cancellationToken)
        {
            var successfulPayments = await _paymentDetailsRepository.GetSuccessfulPayments();
            return _mapper.Map<List<GetSuccesfulPaymentsResponse>>(successfulPayments);
        }
    }
}
