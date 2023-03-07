using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using HotelWeb.Utils;
using System.ComponentModel.DataAnnotations;
using System;

namespace HotelWeb.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountRepository accountRepository;
        private readonly IConfiguration configuration;

        public RegisterModel(IAccountRepository _customerRepository,
                            IConfiguration configuration)
        {
            accountRepository = _customerRepository;
            this.configuration = configuration;
        }
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
        [Required(ErrorMessage = "Confirm Password is required")] 
        public string Password { get; set; }
        [BindProperty]
        public Account Account { get; set; }
        public IActionResult OnPostCreate()
        {
     
          
                try
                {
                    Account acc = new Account
                    {
                        Id = Account.Id,
                        Email = Account.Email,
                        Password = Password,
                        Fullname = Account.Fullname,
                        Gender = true,
                        Phone = "",
                        IsEmployee = false,
                        Avartar = "avatar .webp",
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
    }
}
