﻿using Discount.Grpc.Protos;
using Discount.Grpc.Repository;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        ICouponRepository _couponRepository;
        ILogger<DiscountService> _logger;
        public DiscountService(ICouponRepository couponRepository, ILogger<DiscountService> logger) 
        { 
            _couponRepository = couponRepository;
            _logger = logger;
        }

        public override async Task<CouponRequest> GetDisount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetDiscount(request.ProductId);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,"Discount nof found"));
            }
            _logger.LogInformation("Discount is retrive for ProductName: {productName},Amount: {amount}", coupon.ProductName, coupon.Amount);
            return new CouponRequest { ProductId = request.ProductId,ProductName=coupon.ProductName,Description=coupon.Description,Amount=coupon.Amount};
        }
    }
}
