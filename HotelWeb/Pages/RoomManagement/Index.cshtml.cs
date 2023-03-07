using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using HotelWeb.Utils.FileUploadService;
using System.ComponentModel.DataAnnotations;
using HotelWeb.Validation;

namespace HotelWeb.Pages.RoomManagement
{
    public class IndexModel : PageModel
    {
        private readonly IRoomRepository roomRepository;
        private readonly IRoomTypeRepository roomTypeRepository;
        private readonly IFileUploadService fileUploadService;
        private readonly IBookRoomRepository bookRoomRepository;
        private readonly IRoomInBookingRepository roomInBookingRepository;
        public IndexModel(IRoomRepository _roomRepository, IRoomTypeRepository _roomTypeRepository, IFileUploadService _fileUploadService, IBookRoomRepository _bookRoomRepository, IRoomInBookingRepository _roomInBookingRepository)
        {
            roomRepository = _roomRepository;
            roomTypeRepository = _roomTypeRepository;
            fileUploadService = _fileUploadService;
            bookRoomRepository = _bookRoomRepository;
            roomInBookingRepository = _roomInBookingRepository;
        }
        [BindProperty]
        public Room Room { get; set; }
        public IList<BookRoom> RoomBookingList { get; set; }
        [Required(ErrorMessage = " image can not be empty")]
        [DataType(DataType.Upload)]
        [Display(Name = "Choose an image to upload")]
        [ProductImageValidation]
        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public IList<Room> RoomList { get; set; }
        public IList<RoomType> RoomTypeList { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public DateTime today = DateTime.Now;
        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
        public List<Room> first = new List<Room>();
        public IList<Room> RoomNotEmpty { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("ROLE") != "AD")
            {
                return RedirectToPage("./Index");
            }
            try
            {
                CheckOut = DateTime.Now.AddDays(1);
             
                RoomList = roomRepository.GetRooms().ToList();
                RoomTypeList = roomTypeRepository.GetRoomsType().ToList();
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
                Room room = new Room
                {
                    Id = Room.Id,
                    Title = Room.Title,
                    Image = ImageFile.FileName,
                    Amount = Room.Amount,
                    Description = Room.Description,
                    RoomTypeId = Room.RoomTypeId,
                    Active = true,
                   
                    
                };
                roomRepository.AddRoom(room);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToPage("./Index");
            }


        }
        public IActionResult OnPostSearch([FromForm] string SearchString)
        {

            if (!string.IsNullOrEmpty(SearchString))
            {
                RoomList = roomRepository.SearchByName(SearchString.ToLower().Trim());
                RoomTypeList = roomTypeRepository.GetRoomsType().ToList();
            }
            else
            {
                RoomList = roomRepository.GetRooms().ToList();
                RoomTypeList = roomTypeRepository.GetRoomsType().ToList();
            }
            return Page();
        }
        public IActionResult OnPostUpdateActive(int id)
        {
            try
            {
              
                RoomBookingList = bookRoomRepository.GetStatus(today, today.AddMonths(1)).Where(x => x.Status == "BOOKED" || x.Status == "CHECKIN" || x.Status == "CHECKOUT").ToList();
                foreach (var item in RoomBookingList)
                {
                    RoomNotEmpty = roomInBookingRepository.GetRoomInBooking(item.Id);
                    first.AddRange(RoomNotEmpty);
                }
                if (RoomNotEmpty.Any(x => x.Id == id) == true)
                {
                    TempData["Message"] = "Not Maintenance because Room have Booked";
                    return RedirectToPage("./Index");

                }
                else
                {
                    Room room = roomRepository.GetRoomById(Room.Id);
                    roomRepository.UpdateActive(room.Id, room.Active);
                    TempData["Message"] = "Successfully";
                    return RedirectToPage("./Index");
                }
                

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToPage("./Index");

        }
        public IActionResult OnPostDelete(int id)
        {
            try
            {
                Room room = roomRepository.GetRoomById(id);
                roomRepository.DeleteRoom(room);

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToPage("./Index");

        }
    }
}
