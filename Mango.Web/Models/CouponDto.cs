 using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class CouponDTo
    {
        public int CouponId { get; set; }

        [Required]
        public string CouponCode { get; set; }
       
        public int DiscountCoupon { get; set; }
        public int MinAmount { get; set; }
    }
}
