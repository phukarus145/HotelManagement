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
using HotelWeb.Utils.FileUploadService;
using System.ComponentModel.DataAnnotations;
using HotelWeb.Validation;

namespace HotelWeb.Pages.ServiceManagement
{
    public class EditModel : PageModel
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IServiceTypeRepository serviceTypeRepository;
        private readonly IFileUploadService fileUploadService;
        public EditModel(IServiceRepository _serviceRepository, IServiceTypeRepository _serviceTypeRepository, IFileUploadService _fileUploadService)
        {
            serviceRepository = _serviceRepository;
            serviceTypeRepository = _serviceTypeRepository;
            fileUploadService = _fileUploadService;
        }
        [BindProperty]
        public IList<ServiceType> ServiceTypeList { get; set; }
        [BindProperty]
        public Service Service { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Choose an image to upload")]
        [ProductImageValidation]
        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("ROLE") != "AD")
            {
                return RedirectToPage("./Index");
            }
            try
            {
                Service = serviceRepository.GetServiceById(id);
                ServiceTypeList = serviceTypeRepository.GetServicesType().ToList();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }



            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPost()
        {
            try
            {
                Service service = new Service
                {
                    Id = Service.Id,
                    Title = Service.Title,
                    Image = Service.Image,
                    Amount = Service.Amount,
                    InStock = Service.InStock,
                    ServiceTypeId = Service.ServiceTypeId,
                   Active = Service.Active
                };
                if (ImageFile != null)
                {
                    await fileUploadService.UploadFileAsync(ImageFile);
                    service.Image = ImageFile.FileName;
                }
                serviceRepository.UpdateService(service);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToPage("./Index");
        }
    }
}
