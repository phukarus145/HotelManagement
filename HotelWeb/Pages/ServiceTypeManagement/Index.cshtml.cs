using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace HotelWeb.Pages.ServiceTypeManagement
{
    public class IndexModel : PageModel
    {

       
        private readonly IServiceTypeRepository serviceTypeRepository;

        public IndexModel( IServiceTypeRepository _serviceTypeRepository)
        {

            serviceTypeRepository = _serviceTypeRepository;
        }
        [BindProperty]
       public ServiceType ServiceType { get; set; }

        public IList<ServiceType> ServiceTypeList { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("ROLE") != "AD")
            {
                return RedirectToPage("./Index");
            }
            try
            {
                ServiceTypeList = serviceTypeRepository.GetServicesType().ToList();
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
                ServiceType acc = new ServiceType
                {
                    Id = ServiceType.Id,
                    Title = ServiceType.Title,
                    Description = ServiceType.Description,
                    Active = true
                };
                serviceTypeRepository.AddServiceType(ServiceType);

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
                ServiceTypeList = serviceTypeRepository.SearchByName(SearchString.ToLower().Trim());
            }
            else
            {
                ServiceTypeList = serviceTypeRepository.GetServicesType().ToList();
            }
            return Page();
        }
        public IActionResult OnPostDelete(int id)
        {
            try
            {
                ServiceType serviceType = serviceTypeRepository.GetServiceTypeById(id);
                serviceTypeRepository.DeleteServiceType(serviceType);

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToPage("./Index");

        }
    }
}
