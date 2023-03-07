using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.Repository;

namespace HotelWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRoomRepository roomRepository;
        private readonly IRoomTypeRepository roomTypeRepository;

        public IndexModel(IRoomRepository _roomRepository, IRoomTypeRepository _roomTypeRepository)
        {
            roomRepository = _roomRepository;
            roomTypeRepository = _roomTypeRepository;
          

        }
        [BindProperty]
        public Room Room { get; set; }
        public IList<Room> RoomList { get; set; }
        public IList<RoomType> RoomTypeList { get; set; }
        public IActionResult OnGet()
        {           
            try
            {
                RoomList = roomRepository.GetRooms().ToList();
                RoomList = RoomList.Where(p => p.Active == true).OrderBy(d => d.Id).Take(6).ToList();

                RoomTypeList = roomTypeRepository.GetRoomsType().ToList();
                RoomTypeList = RoomTypeList.Where(p => p.Active == true).ToList();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }

            return Page();
        }
    }
}
