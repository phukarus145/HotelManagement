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
using HotelWeb.Utils.FileUploadService;
using System.ComponentModel.DataAnnotations;
using HotelWeb.Validation;

namespace HotelWeb.Pages.RoomManagement
{
    public class EditModel : PageModel
    {
        private readonly IRoomRepository roomRepository;
        private readonly IRoomTypeRepository roomTypeRepository;
        private readonly IFileUploadService fileUploadService;
        public EditModel(IRoomRepository _roomRepository, IRoomTypeRepository _roomTypeRepository, IFileUploadService _fileUploadService)
        {
            roomRepository = _roomRepository;
            roomTypeRepository = _roomTypeRepository;
            fileUploadService = _fileUploadService;
        }
        [BindProperty]
        public IList<RoomType> RoomTypeList { get; set; }
        [BindProperty]
        public Room Room { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Choose an image to upload")]
        [ProductImageValidation]
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("ROLE") != "AD")
            {
                return RedirectToPage("./Index");
            }
            try {
                Room = roomRepository.GetRoomById(id);
                RoomTypeList = roomTypeRepository.GetRoomsType().ToList();
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
          


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPost()
        {
            try
            {
                Room room = new Room
                {
                    Id = Room.Id,
                    Title = Room.Title,
                    Image = Room.Image,
                    Amount = Room.Amount,
                    Description = Room.Description,
                    RoomTypeId = Room.RoomTypeId,
                    Active = Room.Active,
              
                };
                if (ImageFile != null)
                {
                    await fileUploadService.UploadFileAsync(ImageFile);
                    room.Image = ImageFile.FileName;
                }
                roomRepository.UpdateRoom(room);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
        


            return RedirectToPage("./Index");
        }
    }
}
