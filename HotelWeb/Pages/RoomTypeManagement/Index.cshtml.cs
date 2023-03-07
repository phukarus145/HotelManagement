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

namespace HotelWeb.Pages.RoomTypeManagement
{
    public class IndexModel : PageModel
    {
        private readonly IRoomTypeRepository roomTypeRepository;

        public IndexModel(IRoomTypeRepository _roomTypeRepository)
        {
            roomTypeRepository = _roomTypeRepository;
        }
        [BindProperty]
        public RoomType RoomType { get; set; }

        public IList<RoomType> RoomTypeList { get; set; }
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
                RoomTypeList = roomTypeRepository.GetRoomsType().ToList();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }

            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                RoomType acc = new RoomType
                {
                    Id = RoomType.Id,
                    Title = RoomType.Title,
                    Description = RoomType.Description,               
                    Active = true
                };
                roomTypeRepository.AddRoomType(RoomType);

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToPage("./Index");

        }
        public IActionResult OnPostSearch([FromForm] string SearchString)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                RoomTypeList = roomTypeRepository.SearchByName(SearchString.ToLower().Trim());
            }
            else
            {
                RoomTypeList = roomTypeRepository.GetRoomsType().ToList();
            }
            return Page();
        }
        public IActionResult OnPostDelete(int id)
        {
            try
            {
                RoomType room = roomTypeRepository.GetRoomTypebyId(id);
                roomTypeRepository.DeleteRoomType(room);

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return RedirectToPage("./Index");

        }
    }
}
