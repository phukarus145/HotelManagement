using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Repository;
using BusinessObject.Model;
using System.ComponentModel.DataAnnotations;
using HotelWeb.Validation;
using Microsoft.AspNetCore.Http;

namespace HotelWeb.Pages.Booking
{
    public class ShoppingModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        private readonly IRoomRepository roomRepository;
        private readonly IRoomTypeRepository roomTypeRepository;
        private readonly IBookRoomRepository bookRoomRepository;
        private readonly IRoomInBookingRepository roomInBookingRepository;

        public ShoppingModel(IRoomRepository _roomRepository, IRoomTypeRepository _roomTypeRepository, IBookRoomRepository _bookRoomRepository, IRoomInBookingRepository _roomInBookingRepository)
        {
            roomRepository = _roomRepository;
            roomTypeRepository = _roomTypeRepository;
            bookRoomRepository = _bookRoomRepository;
            roomInBookingRepository = _roomInBookingRepository;
        }
        [BindProperty]
        public Room Room { get; set; }
        [BindProperty]
        public BookRoom BookRoom { get; set; }
        [DateValidation(CompareToOperation.LessThan, "CheckIn", "CheckOut")]
        [BindProperty]
        [Required(ErrorMessage = "CheckIn can not be empty")]
        [DataType(DataType.Date)]
       
        public DateTime CheckIn { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "CheckOut can not be empty")]
        
        [DataType(DataType.Date)]
        public DateTime CheckOut { get;  set ;  }
        [BindProperty]
        [Required(ErrorMessage = "Status can not be empty")]
        public string status { get; set; }
        public IList<Room> RoomList { get; set; }
        public IList<BookRoom> RoomBookingList { get; set; }
        public IList<Room> RoomNotEmpty { get; set; }
        public IList<Room> RoomInDay { get; set; }
        public IList<RoomType> RoomTypeList { get; set; }
        public List<Room> first = new List<Room>();
        public DateTime today = DateTime.Today;
        public TimeSpan ts;
        public IActionResult OnGet()
        {
            try
            {
                if (HttpContext.Session.GetString("ROLE") == "AD" || HttpContext.Session.GetString("Customer") == "Customer")
                {
                    return RedirectToPage("../Index");
                }
                RoomList = roomRepository.GetRooms().ToList();
                RoomList = RoomList.Where(p => p.Active == true).ToList();
                CheckIn = today;
                CheckOut = today.AddDays(1);
                ts = today.AddDays(1).Subtract(today);
                RoomTypeList = roomTypeRepository.GetRoomsType().ToList();
                RoomTypeList = RoomTypeList.Where(p => p.Active == true).ToList();
              
                RoomBookingList = bookRoomRepository.GetStatus(CheckIn , CheckOut).Where(x => x.Status == "BOOKED" || x.Status == "CHECKIN" || x.Status == "CHECKOUT").ToList();
          
                foreach (var id in RoomBookingList)
                {                  
                     RoomNotEmpty = roomInBookingRepository.GetRoomInBooking(id.Id);
                    first.AddRange(RoomNotEmpty);
                }
               
                //  status = "booked";
                //   status = "maintenance";
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }

            return Page();
        }
        public   IActionResult OnPostSearch([FromForm] string SearchString)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                RoomTypeList = roomTypeRepository.GetRoomsType().ToList();
                RoomTypeList = RoomTypeList.Where(p => p.Active == true).ToList();
                if (Room.RoomTypeId != null)
                {
                    RoomList = roomRepository.SearchByNameWithType(SearchString.ToLower().Trim(), Room.RoomTypeId);
                    RoomList = RoomList.Where(p => p.Active == true).ToList();
                }
                else
                {
                    RoomList = roomRepository.SearchByName(SearchString.ToLower().Trim());
                    RoomList = RoomList.Where(p => p.Active == true).ToList();
                }
                if (CheckIn >= CheckOut || CheckIn < DateTime.Now.AddDays(-1) && CheckOut != CheckIn)
                {
                    CheckIn = today;
                    CheckOut = today.AddDays(1);
                    ts = CheckOut.Subtract(CheckIn);
                    ViewData["ErrorMessage"] = "CheckIn or CheckOut Is valid";
                }
                else
                {
                    CheckIn = CheckIn;
                    CheckOut = CheckOut;
                    ts = CheckOut.Subtract(CheckIn);
                    RoomBookingList = bookRoomRepository.GetStatus(CheckIn, CheckOut).Where(x => x.Status == "BOOKED" || x.Status == "CHECKIN" || x.Status == "CHECKOUT").ToList();

                    foreach (var id in RoomBookingList)
                    {
                         RoomNotEmpty = roomInBookingRepository.GetRoomInBooking(id.Id);
                         first.AddRange(RoomNotEmpty);
                    }                  
                }

            }

            else
            {
                RoomList = roomRepository.GetRooms().ToList();
                RoomList = RoomList.Where(p => p.Active == true).ToList();

                RoomTypeList = roomTypeRepository.GetRoomsType().ToList();
                RoomTypeList = RoomTypeList.Where(p => p.Active == true).ToList();
                if (Room.RoomTypeId != null)
                {
                    RoomList = roomRepository.GetRooms().ToList();
                    RoomList = roomRepository.FilterByTypeId(Room.RoomTypeId);
                    RoomList = RoomList.Where(p => p.Active == true).ToList();
                }
                if (CheckIn >= CheckOut || CheckIn <= DateTime.Now.AddDays(-1) && CheckOut != CheckIn)
                {
                    CheckIn = today;
                    CheckOut = today.AddDays(1);
                    ts = CheckOut.Subtract(CheckIn);
                    ViewData["ErrorMessage"] = "CheckIn or CheckOut Is valid";
                  
                }
                else
                {
                   
                    CheckIn = CheckIn;
                    CheckOut = CheckOut;
                     ts = CheckOut.Subtract(CheckIn);
                    RoomBookingList = bookRoomRepository.GetStatus(CheckIn, CheckOut).Where(x => x.Status == "BOOKED" || x.Status == "CHECKIN" || x.Status == "CHECKOUT").ToList();
                    foreach (var id in RoomBookingList)
                        {
                        RoomNotEmpty = roomInBookingRepository.GetRoomInBooking(id.Id);
                        first.AddRange(RoomNotEmpty);
                        }
                      
                }

            }
           
            return Page();
        }
       
    }
}
