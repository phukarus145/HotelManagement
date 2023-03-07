using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.DAO
{
   public class CouponDAO
    {
        private static CouponDAO instance = null;
        private static readonly object instanceLock = new object();
        private CouponDAO() { }
        public static CouponDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CouponDAO();
                    }
                    return instance;
                }
            }
        }
        public static List<Coupon> GetCoupons()
        {
            var coupons = new List<Coupon>();

            try
            {
                using (var context = new HotelManagementContext())
                {
                    coupons = context.Coupon.ToList();
                }
                return coupons;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void AddCoupon(Coupon m)
        {
            try
            {


                using (var context = new HotelManagementContext())
                {
                    var p1 = context.Room.SingleOrDefault(c => c.Id.Equals(m.Id));

                    if (p1 == null)
                    {
                        if(m.Discount >= 100)
                        {
                            context.Coupon.Add(m);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Discount not > 100%");
                        }                      
                    }
                    else
                    {
                        throw new Exception("Coupon đã tồn tại");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCoupon(Coupon m)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {

                    context.Entry<Coupon>(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteCoupon(Coupon p)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {
                    var p1 = context.Coupon.SingleOrDefault(c => c.Id == p.Id);
                    context.Coupon.Remove(p1);

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IList<Coupon> SearchByName(string search)
        {
            IList<Coupon> searchResult = null;

            using (var context = new HotelManagementContext())
            {
                var searchValues = from coupon in context.Coupon
                                   where coupon.Id.ToLower().Contains(search.ToLower())
                                   select coupon;

                searchResult = searchValues.ToList();
            }
            return searchResult;
        }
        public Coupon GetCouponById(string Id)
        {

            IEnumerable<Coupon> coupons = GetCoupons();
            Coupon coupon = coupons.SingleOrDefault(mb => mb.Id == Id);
            return coupon;
        }
        
    }
}
