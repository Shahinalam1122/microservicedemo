using AutoMapper;
using Discount.Grpc.Protos;
using Discount.Grpc.Repository;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        ICouponRepository _couponRepository;
        ILogger<DiscountService> _logger;
        IMapper _mapper;
        public DiscountService(ICouponRepository couponRepository, ILogger<DiscountService> logger, IMapper mapper) 
        { 
            _couponRepository = couponRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponRequest> GetDisount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetDiscount(request.ProductId);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,"Discount nof found"));
            }
            _logger.LogInformation("Discount is retrive for ProductName: {productName},Amount: {amount}", coupon.ProductName, coupon.Amount);
            //return new CouponRequest { ProductId = request.ProductId,ProductName=coupon.ProductName,Description=coupon.Description,Amount=coupon.Amount};
            return _mapper.Map<CouponRequest>(coupon);
        }
    }
}
