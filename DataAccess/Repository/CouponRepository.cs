using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;

namespace DataAccess.Repository
{
    public class CouponRepository : ICouponRepository
    {
        public IEnumerable<Coupon> GetCoupons() => CouponDAO.GetCoupons();

        public Coupon GetCouponById(string ID) => CouponDAO.Instance.GetCouponById(ID);
        public void DeleteCoupon(Coupon m) => CouponDAO.DeleteCoupon(m);

        public void AddCoupon(Coupon m) => CouponDAO.AddCoupon(m);
        public void UpdateCoupon(Coupon m) => CouponDAO.UpdateCoupon(m);
        public IList<Coupon> SearchByName(string search) => CouponDAO.Instance.SearchByName(search);
     
    }
}
