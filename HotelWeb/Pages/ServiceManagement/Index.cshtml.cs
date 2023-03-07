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
using HotelWeb.Utils.FileUploadService;
using System.ComponentModel.DataAnnotations;
using HotelWeb.Validation;

namespace HotelWeb.Pages.ServiceManagement
{
    public class IndexModel : PageModel
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IServiceTypeRepository serviceTypeRepository;
        private readonly IFileUploadService fileUploadService;
        public IndexModel(IServiceRepository _serviceRepository, IServiceTypeRepository _serviceTypeRepository, IFileUploadService _fileUploadService)
        {
            serviceRepository = _serviceRepository;
            serviceTypeRepository = _serviceTypeRepository;
            fileUploadService = _fileUploadService;

        }
        [BindProperty]
        public Service Service { get; set; }

        [Required(ErrorMessage = " image can not be empty")]
        [DataType(DataType.Upload)]
        [Display(Name = "Choose an image to upload")]
        [ProductImageValidation]
        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public IList<Service> ServiceList { get; set; }
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
                ServiceList = serviceRepository.GetServices().ToList();
                ServiceTypeList = serviceTypeRepository.GetServicesType().ToList();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }

            return Page();
        }
        public async Task<IActionResult> OnPostCreate()
        {

            try
            {
                string filePath = await fileUploadService.UploadFileAsync(ImageFile);
                Service service = new Service
                {
                    Id = Service.Id,
                    Title = Service.Title,
                    Image = ImageFile.FileName,
                    Amount = Service.Amount,
                    InStock = Service.InStock,
                    ServiceTypeId = Service.ServiceTypeId,
                    Active = true
                };
                serviceRepository.AddService(service);

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
                ServiceList = serviceRepository.SearchByName(SearchString.ToLower().Trim());
                ServiceTypeList = serviceTypeRepository.GetServicesType().ToList();
            }
            else
            {
                ServiceList = serviceRepository.GetServices().ToList();
                ServiceTypeList = serviceTypeRepository.GetServicesType().ToList();
            }
            return Page();
        }
        public IActionResult OnPostDelete(int id)
        {
            try
            {
                Service service = serviceRepository.GetServiceById(id);
                serviceRepository.DeleteService(service);

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToPage("./Index");

        }
        public IActionResult OnPostUpdateActive()
        {
            try
            {
                Service service = serviceRepository.GetServiceById(Service.Id);
                serviceRepository.UpdateActive(service.Id, service.Active);

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToPage("./Index");

        }
    }
}
