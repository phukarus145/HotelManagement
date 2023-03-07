using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.Repository;
using HotelWeb.Utils.FileUploadService;
using HotelWeb.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelWeb.Pages.Profile
{
    public class MyProfileModel : PageModel
    {

        private readonly IAccountRepository accountRepository;
        private readonly IFileUploadService fileUploadService;
        public MyProfileModel(IAccountRepository _accountRepository, IFileUploadService _fileUploadService)
        {
            accountRepository = _accountRepository;
            fileUploadService = _fileUploadService;
        }
        [BindProperty]
        public Account Account { get; set; }


        [DataType(DataType.Upload)]
        [Display(Name = "Choose an image to upload")]
        [ProductImageValidation]
        [BindProperty]
        public IFormFile ImageFile { get; set; }

      
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("ROLE") == null)
            {
                return Page();
            }
            try
            {
                string Id = HttpContext.Session.GetString("Id");
                Account = accountRepository.GetProfile(int.Parse(Id));
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
         
                Account acc = new Account
                {
                    Id = Account.Id,
                    Email = Account.Email,
                    Password = Account.Password,
                    Fullname = Account.Fullname,
                    Gender = Account.Gender,
                    Phone = Account.Phone,
                    IsEmployee = Account.IsEmployee,
                    Avartar = Account.Avartar,
                    Active = Account.Active
                    
                };
                if (ImageFile != null)
                {
                    await fileUploadService.UploadFileAsync(ImageFile);
                    acc.Avartar = ImageFile.FileName;
                }
                HttpContext.Session.SetString("Avatar", acc.Avartar);
                HttpContext.Session.SetString("EMAIL", acc.Fullname);
                accountRepository.UpdateMember(acc);
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
        }

    }
}
