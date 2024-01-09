using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI
{
    public class Mapping
    {
        public static MapperConfiguration RegisterMaps2()
        {
            var mapconfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponDTo>();
                config.CreateMap<CouponDTo, Coupon>();
            });
            return mapconfig;
        }
    }
}
