using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.GetAllPayments
{
    public class GetAllPaymentsHandler : IRequestHandler<GetAllPaymentsRequest, List<GetAllPaymentsResponse>>
    {
        private readonly IPaymentDetailsRepository _paymentDetailsRepository;
        private readonly IMapper _mapper;
        public GetAllPaymentsHandler(IPaymentDetailsRepository paymentDetailsRepository, IMapper mapper)
        {
            _paymentDetailsRepository = paymentDetailsRepository;
            _mapper = mapper;
        }
        public async Task<List<GetAllPaymentsResponse>> Handle(GetAllPaymentsRequest request, CancellationToken cancellationToken)
        {
            var payments = await _paymentDetailsRepository.GetAllPayments();
            return _mapper.Map<List<GetAllPaymentsResponse>>(payments);
        }
    }
}
