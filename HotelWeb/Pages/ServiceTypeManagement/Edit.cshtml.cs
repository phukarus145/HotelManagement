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
using Microsoft.AspNetCore.Http;

namespace HotelWeb.Pages.ServiceTypeManagement
{
    public class EditModel : PageModel
    {
        private readonly IServiceTypeRepository serviceTypeRepository;

        public EditModel(IServiceTypeRepository _serviceTypeRepository)
        {
            serviceTypeRepository = _serviceTypeRepository;
        }

        [BindProperty]
        public ServiceType ServiceType { get; set; }

        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("ROLE") != "AD")
            {
                return RedirectToPage("./Index");
            }
            try
            {
                ServiceType = serviceTypeRepository.GetServiceTypeById(id);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
         


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            try
            {

                serviceTypeRepository.UpdateServiceType(ServiceType);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToPage("./Index");
        }
    }
}
