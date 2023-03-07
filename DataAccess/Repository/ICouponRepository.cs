using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.Repository
{
    public interface ICouponRepository
    {
        IEnumerable<Coupon> GetCoupons();

        Coupon GetCouponById(string Id);

        void DeleteCoupon(Coupon m);

        void UpdateCoupon(Coupon m);
        void AddCoupon(Coupon m);
        IList<Coupon> SearchByName(string search);
    
    }
}
