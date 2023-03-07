using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using DataAccess.Repository;

namespace HotelWeb.Pages.CouponManagement
{
    public class IndexModel : PageModel
    {

        private readonly ICouponRepository couponRepository;

        public IndexModel(ICouponRepository _couponRepository)
        {

            couponRepository = _couponRepository;
        }
        [BindProperty]
        public Coupon Coupon { get; set; }

        public IList<Coupon> CouponList { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                CouponList = couponRepository.GetCoupons().ToList();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }

            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                Coupon coupon = new Coupon
                {
                    Id = Coupon.Id,               
                    Description = Coupon.Description,
                    Discount = Coupon.Discount
                };
                couponRepository.AddCoupon(coupon);

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToPage("./Index");

        }
        public IActionResult OnPostSearch([FromForm] string SearchString)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                CouponList = couponRepository.SearchByName(SearchString.ToLower().Trim());
            }
            else
            {
                CouponList = couponRepository.GetCoupons().ToList();
            }
            return Page();
        }
        public IActionResult OnPostDelete(string id)
        {
            try
            {
                Coupon couponid = couponRepository.GetCouponById(id);
   
                couponRepository.DeleteCoupon(couponid);

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToPage("./Index");

        }
    }
}
