using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Model;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace HotelWeb.Pages.RoomTypeManagement
{
    public class EditModel : PageModel
    {
        private readonly IRoomTypeRepository roomTypeRepository;

        public EditModel(IRoomTypeRepository _roomRepository)
        {
            roomTypeRepository = _roomRepository;
        }

        [BindProperty]
        public RoomType RoomType { get; set; }

        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("ROLE") != "AD")
            {
                return RedirectToPage("./Index");
            }     
            try
            {
                RoomType = roomTypeRepository.GetRoomTypebyId(id);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            try
            {
                roomTypeRepository.UpdateRoomType(RoomType);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }       
            return RedirectToPage("./Index");
        }
    }
}
