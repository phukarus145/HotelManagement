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
    public class ChangePasswordModel : PageModel
    {
        private readonly IAccountRepository accountRepository;
        private readonly IFileUploadService fileUploadService;
        public ChangePasswordModel(IAccountRepository _accountRepository, IFileUploadService _fileUploadService)
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

        [BindProperty]
        [StringLength(maximumLength: 20,
 ErrorMessage = "Password's lenght must be between 6-20",
 MinimumLength = 6)]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        [StringLength(maximumLength: 20,
        ErrorMessage = "Password's lenght must be between 6-20",
        MinimumLength = 6)]
        [Required(ErrorMessage = " Password is required")]
        public string Password { get; set; }

        [BindProperty]
        [StringLength(maximumLength: 20,
        ErrorMessage = "Password's lenght must be between 6-20",
        MinimumLength = 6)]
        [Required(ErrorMessage = "Current Password is required")]
        public string CurrentPassword { get; set; }
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

                if (CurrentPassword == Account.Password)
                {
                    ViewData["Message"] = "Successfully";
                    accountRepository.ChangePassword(Account.Id, Password);
                    return Page();
                }
                else
                {
                    ViewData["ErrorMessage"] = "Current Password not same";                   
                    return Page();
                }

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
