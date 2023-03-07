using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelWeb.Pages.Profile
{
    public class HistoryModel : PageModel
    {
        private readonly IBookRoomRepository bookRoomRepository;
        private readonly IRoomInBookingRepository roomInBookingRepository;
        private readonly IRoomRepository roomRepository;
        public HistoryModel(IRoomRepository _roomRepository, IBookRoomRepository _bookRoomRepository, IRoomInBookingRepository _roomInBookingRepository )
        {
           
            bookRoomRepository = _bookRoomRepository;
            roomInBookingRepository = _roomInBookingRepository;
            roomRepository = _roomRepository;
        }
        public IList<BookRoom> BookRoom { get; set; }
        public IList<RoomInBooking> RoomInBookings { get; set; }
        public IList<Room> Room { get; set; }
        public IActionResult OnGet()
        {
            try
            {
                if (HttpContext.Session.GetString("ROLE") != "Customer")
                {
                    return RedirectToPage("./Login");
                }
                BookRoom = bookRoomRepository.GetHistory(int.Parse(HttpContext.Session.GetString("Id")));
                RoomInBookings = roomInBookingRepository.GetRooms().ToList();
                Room = roomRepository.GetRooms().ToList();
            }
            catch
            {

            }
            return Page();
        }
        public IActionResult OnPostCancel(int id)
        {
            try
            {
                bookRoomRepository.UpdateStatusCancel(id);
                TempData["Message"] = "Cancel Finsish";
                return RedirectToPage("./History");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToPage("./History");
            }
        }
    }
}
