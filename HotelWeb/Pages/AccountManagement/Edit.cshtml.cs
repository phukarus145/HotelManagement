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

namespace HotelWeb.Pages.AccountManagement
{
    public class EditModel : PageModel
    {
        private readonly IAccountRepository accountRepository;
        private readonly IFileUploadService fileUploadService;
        public EditModel(IAccountRepository _accountRepository, IFileUploadService _fileUploadService)
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


        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("ROLE") != "AD")
            {
                return RedirectToPage("./Index");
            }
            try
            {
                Account = accountRepository.GetProfile(id);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
          

           
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult>  OnPost()
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
                accountRepository.UpdateMember(acc);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
          


            return RedirectToPage("./Index");
        }

      
    }
}
