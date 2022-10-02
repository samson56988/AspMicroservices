using DiscountGrpc.Protos;
using DiscountGrpc.Repositories;
using Grpc.Core;

namespace DiscountGrpc.Services
{
    public class DiscountService:DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository discountRepository , ILogger<DiscountService> logger)
        {
            _repository = discountRepository;
            _logger = logger;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);

            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            }

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
    }
}
