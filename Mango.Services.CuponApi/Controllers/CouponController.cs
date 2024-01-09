using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _map;

        public CouponController(AppDbContext db, IMapper map)
        {
            this._db = db;  
            this._responseDto = new ResponseDto();
            this._map= map;
        }


        [HttpGet]
        [Route("GetAllCoupons")]
        public ResponseDto Get()
        {
            try
            {
                //Bogdan nu trebuia IQuerable???
                IQueryable<Coupon> couponList = _db.Coupons;
                _responseDto.Result = _map.Map<IEnumerable<CouponDTo>> (couponList);              
            }
            catch (Exception ex)
            {
                _responseDto.IsSUcess = false;
                 _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }


        [HttpGet]
        [Route("GetCouponByID:/{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon Onecoupon = _db.Coupons.Where(x => x.CouponId == id).First();
                _responseDto.Result = _map.Map<CouponDTo>(Onecoupon);
               
            }
            catch (Exception ex)
            {
                _responseDto.IsSUcess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }


        [HttpGet]
        [Route("GetCouponByCode:/{code}")]
        public ResponseDto Get(string code)
        {
            try
            {
                Coupon Onecoupon = _db.Coupons.Where(x => x.CouponCode == code).First();
                _responseDto.Result = _map.Map<CouponDTo>(Onecoupon);

            }
            catch (Exception ex)
            {
                _responseDto.IsSUcess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }


        [HttpPost]
        [Route("AddNewCoupon")]
        public ResponseDto Post([FromBody] CouponDTo couponDto)
        {
            try
            {
                Coupon postCoupon = _map.Map<Coupon>(couponDto);
                _db.Coupons.Add(postCoupon);
                _db.SaveChanges();
                 _responseDto.Result = _map.Map<CouponDTo>(postCoupon);
            }
            catch (Exception ex)
            {
                _responseDto.IsSUcess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }


        [HttpPut]
        public ResponseDto Put([FromBody] CouponDTo couponDto)
        {
            try
            {
                Coupon postCoupon = _map.Map<Coupon>(couponDto);
                _db.Coupons.Update(postCoupon);
                _db.SaveChanges();
                _responseDto.Result = _map.Map<CouponDTo>(postCoupon);
            }
            catch (Exception ex)
            {
                _responseDto.IsSUcess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }


        [HttpDelete]
        [Route("DeteleCouponByID:/{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(x => x.CouponId == id);
                _db.Coupons.Remove(coupon);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _responseDto.IsSUcess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }


    }
}
