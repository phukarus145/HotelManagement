using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using DataAccess.Repository;

namespace HotelWeb.Pages.CouponManagement
{
    public class EditModel : PageModel
    {
        private readonly ICouponRepository couponRepository;

        public EditModel(ICouponRepository _couponRepository)
        {
            couponRepository = _couponRepository;
        }

        [BindProperty]
        public Coupon Coupon { get; set; }

        public IActionResult OnGet(string id)
        {

            Coupon = couponRepository.GetCouponById(id);


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {

            couponRepository.UpdateCoupon(Coupon);


            return RedirectToPage("./Index");
        }
    }
}
