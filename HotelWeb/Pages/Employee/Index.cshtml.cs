using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.Repository;
using HotelWeb.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelWeb.Pages.Employee
{
    public class IndexModel : PageModel
    {
        private readonly IRoomRepository roomRepository;
        private readonly IRoomTypeRepository roomTypeRepository;
        private readonly IBookRoomRepository bookRoomRepository;
        private readonly IRoomInBookingRepository roomInBookingRepository;

        public IndexModel(IRoomRepository _roomRepository, IRoomTypeRepository _roomTypeRepository, IBookRoomRepository _bookRoomRepository, IRoomInBookingRepository _roomInBookingRepository)
        {
            roomRepository = _roomRepository;
            roomTypeRepository = _roomTypeRepository;
            bookRoomRepository = _bookRoomRepository;
            roomInBookingRepository = _roomInBookingRepository;
        }
        [BindProperty]
        public Room Room { get; set; }

   
        public IList<Room> RoomList { get; set; }
        public IList<Room> RoomNotEmpty { get; set; }
        public IList<Room> RoomBooked { get; set; }
        public IList<RoomInBooking> RoomInBookingList { get; set; }
        public IList<BookRoom> BookRoomList { get; set; }
        public IList<BookRoom> BookRoomCheckInList { get; set; }
        public IList<BookRoom> BookRoomBookedList { get; set; }
        public IList<BookRoom> BookRoomClearList { get; set; }
        public IList<BookRoom> BookRoomInDayList { get; set; }
        public IList<RoomType> RoomTypeList { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public List<Room> CheckIn = new List<Room>();
        public List<Room> Booked = new List<Room>();
        public List<Room> Clear = new List<Room>();
        public List<RoomInBooking> RoomBookedInDay = new List<RoomInBooking>();
        public List<RoomInBooking> RoomCheckInInDay = new List<RoomInBooking>();
        public List<RoomInBooking> RoomClearInInDay = new List<RoomInBooking>();
        public DateTime toDay = DateTime.Now;
        
        public IActionResult OnGet()
        {          
            try
            {
       
                RoomInBookingList = roomInBookingRepository.GetRooms().ToList();
                BookRoomList = bookRoomRepository.GetBookRooms();
                BookRoomCheckInList = bookRoomRepository.GetListStatusCheckInRoomInDay().Where(s => s.Status == "CHECKIN").ToList();
                BookRoomBookedList = bookRoomRepository.GetListStatusCheckInRoomInDay().Where(s => s.Status == "BOOKED").ToList();
                BookRoomClearList = bookRoomRepository.GetListStatusCheckInRoomInDay().Where(s => s.Status == "CHECKOUT").ToList();
                RoomList = roomRepository.GetRooms().ToList();
                RoomList = RoomList.Where(p => p.Active == true).ToList();
                RoomTypeList = roomTypeRepository.GetRoomsType().ToList();
                foreach (var id in BookRoomCheckInList)
                {
                    RoomInBookingList = roomInBookingRepository.GetRoomInBookingByRoom(id.Id);
                    RoomCheckInInDay.AddRange(RoomInBookingList);
                }
                foreach (var id in BookRoomBookedList)
                {
                    RoomInBookingList = roomInBookingRepository.GetRoomInBookingByRoom(id.Id);
                    RoomBookedInDay.AddRange(RoomInBookingList);
                }
                foreach (var id in BookRoomClearList)
                {
                    RoomInBookingList = roomInBookingRepository.GetRoomInBookingByRoom(id.Id);
                    RoomClearInInDay.AddRange(RoomInBookingList);
                }
                foreach (var id in BookRoomCheckInList)
                {
                    RoomNotEmpty = roomInBookingRepository.GetRoomInBooking(id.Id);
                    CheckIn.AddRange(RoomNotEmpty);
                }
               
                    foreach (var id in BookRoomBookedList)
                    {
                        RoomBooked = roomInBookingRepository.GetRoomInBooking(id.Id);
                        Booked.AddRange(RoomBooked);
                    }
                foreach (var id in BookRoomClearList)
                {
                    RoomBooked = roomInBookingRepository.GetRoomInBooking(id.Id);
                    Clear.AddRange(RoomBooked);
                }




            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }

            return Page();
        }
        public IActionResult OnPostClear(int id)
        {
            try
            {
                bookRoomRepository.UpdateStatusFinish(id);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return Page();

            }
        }
    }
}
