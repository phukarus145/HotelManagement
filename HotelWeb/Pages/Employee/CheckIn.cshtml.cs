using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelWeb.Pages.Employee
{
    public class CheckInModel : PageModel
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IServiceTypeRepository serviceTypeRepository;
        private readonly IRoomRepository roomRepository;
        private readonly IRoomTypeRepository roomTypeRepository;
        private readonly IBookRoomRepository bookRoomRepository;
        private readonly IRoomInBookingRepository roomInBookingRepository;
        private readonly IServiceInRoomRepository serviceInRoomRepository;
        private readonly IAccountRepository accountRepository;


        public CheckInModel(IAccountRepository _accountRepository, IServiceInRoomRepository _serviceInRoomRepository, IRoomRepository _roomRepository, IRoomTypeRepository _roomTypeRepository, IBookRoomRepository _bookRoomRepository, IRoomInBookingRepository _roomInBookingRepository, IServiceRepository _serviceRepository, IServiceTypeRepository _serviceTypeRepository)
        {
            serviceRepository = _serviceRepository;
            serviceTypeRepository = _serviceTypeRepository;
            roomRepository = _roomRepository;
            roomTypeRepository = _roomTypeRepository;
            bookRoomRepository = _bookRoomRepository;
            roomInBookingRepository = _roomInBookingRepository;
            serviceInRoomRepository = _serviceInRoomRepository;
            accountRepository = _accountRepository;
        }
        [BindProperty]
        public BookRoom BookRoom { get; set; }
        [BindProperty]
        public Account Account { get; set; }
        public IList<Account> AccountList { get; set; }
        [BindProperty]
        public decimal cmnd { get; set; }
        public IActionResult OnGet(int id)
        {
            try
            {
                AccountList = accountRepository.GetMembers().ToList();
                BookRoom = bookRoomRepository.GetBookRoomById(id);
                Account = AccountList.SingleOrDefault(x => x.Id == BookRoom.AccountId);

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }

            return Page();
        
        }
        public IActionResult OnPost(int id, decimal cmnd)
        {
            try
            {
                bookRoomRepository.UpdateStatusCheckIn(id, cmnd);
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
