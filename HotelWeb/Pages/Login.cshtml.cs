using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using HotelWeb.Utils;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HotelWeb.Pages
{
    public class LoginModel : PageModel
    {
            private readonly IAccountRepository accountRepository;
            private readonly IConfiguration configuration;

            public LoginModel(IAccountRepository _customerRepository,
                                IConfiguration configuration)
            {
                accountRepository = _customerRepository;
                this.configuration = configuration;
            }

        [BindProperty]
        [Required]
        [EmailAddress(ErrorMessage = "Wrong format for email address")]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var adminEmail = configuration.GetSection("AdminAccount").GetSection("Email").Value;
                var adminPassword = configuration.GetSection("AdminAccount").GetSection("Password").Value;

                if (Email.Equals(adminEmail) && Password.Equals(adminPassword))
                {
                    HttpContext.Session.SetString("EMAIL", Email);
                    HttpContext.Session.SetString("Avatar", "avatar .webp");
                    HttpContext.Session.SetString("ROLE", "AD");
                    return RedirectToPage("./Index");
                }
                else
                {

                    Account customer = accountRepository.LoginMember(Email, Password);
                    if (customer != null)
                    {
                        if (customer.Active == true)
                        {
                            if (customer.IsEmployee == false)
                            {
                                HttpContext.Session.SetString("Id", customer.Id.ToString());
                                HttpContext.Session.SetString("Avatar", customer.Avartar);
                                HttpContext.Session.SetString("EMAIL", customer.Fullname);
                                HttpContext.Session.SetString("ROLE", "Customer");
                                return RedirectToPage("./Index");
                            }
                            else
                            {
                                HttpContext.Session.SetString("Id", customer.Id.ToString());
                                HttpContext.Session.SetString("Avatar", customer.Avartar);
                                HttpContext.Session.SetString("EMAIL", customer.Fullname);
                                HttpContext.Session.SetString("ROLE", "Employee");
                                return RedirectToPage("./Index");
                            }
                        }
                        else {
                            ViewData["ErrorMessage"] = "Account Blocked, contact with Admin.";
                            return Page();
                        }
                           
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Wrong email or password.";
                        return Page();
                    }
                }


            }
            else
            {
                return Page();
            }
        }
    }
}
