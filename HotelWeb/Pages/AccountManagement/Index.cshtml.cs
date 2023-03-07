using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using DataAccess.Repository;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using HotelWeb.Utils.FileUploadService;
using HotelWeb.Validation;
namespace HotelWeb.Pages.AccountManagement
{
    public class IndexModel : PageModel
    {
       

        private readonly IAccountRepository accountRepository;
        private readonly IFileUploadService fileUploadService;
        public IndexModel(IAccountRepository _accountRepository, IFileUploadService _fileUploadService)
        {
            accountRepository = _accountRepository;
            fileUploadService = _fileUploadService;
        }
        [BindProperty]
        public Account Account { get; set; }

       
        [Required(ErrorMessage = " image can not be empty")]
        [DataType(DataType.Upload)]
        [Display(Name = "Choose an image to upload")]
        [ProductImageValidation]
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public IList<Account> AccountList { get; set; }
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
                AccountList = accountRepository.GetMembers().ToList();
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
                Account acc = new Account
                {
                    Id = Account.Id,
                    Email = Account.Email,
                    Password = Account.Password,
                    Fullname = Account.Fullname,
                    Gender = Account.Gender,
                    Phone = Account.Phone,
                    IsEmployee = Account.IsEmployee,
                    Avartar = ImageFile.FileName,
                    Active = true
                };
                accountRepository.AddMember(acc);

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
        }
        public IActionResult OnPostSearch([FromForm] string SearchString)
        {
            try
            {
                if (!string.IsNullOrEmpty(SearchString))
                {
                    AccountList = accountRepository.SearchByName(SearchString.ToLower().Trim());
                    return Page();
                }
                else
                {
                    AccountList = accountRepository.GetMembers().ToList();
                    return Page();
                }
               
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return Page();
        }
            public IActionResult OnPostUpdateActive()
            {
            try
            {
             Account acc = accountRepository.GetProfile(Account.Id);
             accountRepository.UpdateActive(acc.Id, acc.Active);

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToPage("./Index");

            }
        public IActionResult OnPostDelete()
        {
            try
            {
                Account acc = accountRepository.GetProfile(Account.Id);
                accountRepository.DeleteMember(acc);

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToPage("./Index");

        }
    }
}