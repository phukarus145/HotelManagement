using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.Repository;
using HotelWeb.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


namespace HotelWeb.Pages.Booking
{
    public class RoomDetailModel : PageModel
    {
        private readonly IRoomRepository roomRepository;
        private readonly IRoomTypeRepository roomTypeRepository;
        
        public RoomDetailModel(IRoomRepository _roomRepository, IRoomTypeRepository _roomTypeRepository)
        {
            roomRepository = _roomRepository;
            roomTypeRepository = _roomTypeRepository;
          
        }
        [BindProperty]
        [Required(ErrorMessage = "CheckIn can not be empty")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }
        [BindProperty]
        public IList<RoomType> RoomTypeList { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "CheckOut can not be empty")]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
        [BindProperty]
        public Room Room { get; set; }
        public TimeSpan ts;
        public  IActionResult OnGet(int id,DateTime In, DateTime Out)
        {
           
            try
            {
                RoomTypeList = roomTypeRepository.GetRoomsType().ToList();
                Room = roomRepository.GetRoomById(id);      
                CheckIn = In;
                CheckOut = Out;
                ts = CheckOut.Subtract(CheckIn);
             
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return Page();
        }
        public IActionResult OnPostAddtoCart()
        {   
            try
            {
                ts = CheckOut.Subtract(CheckIn);
                var cart = HttpContext.Session.GetComplexData<List<Cart>>("CART");
                var item = new Cart
                {
                    RoomId = Room.Id,
                    Amount = Room.Amount * decimal.Parse(ts.Days.ToString()),
                    Image = Room.Image,
                    CheckIn = CheckIn,
                    CheckOut = CheckOut,
                    Title = Room.Title
                };
                if (cart != null)
                {
                    Cart Day = cart.FirstOrDefault(i => i.CheckIn.Equals(CheckIn) && i.CheckOut.Equals(CheckOut));
                    Cart Id = cart.SingleOrDefault(i => i.RoomId.Equals(Room.Id));
                    if (Id != null )
                    {
                        // If item is already exist in cart
                      //  Cart itemIdInCart = cart.SingleOrDefault(i => i.CheckIn.Equals(item.CheckIn) && i.CheckOut.Equals(item.CheckOut));
                      //  Cart itemDayInCart =   cart.SingleOrDefault(i => i.CheckIn.Equals(item.CheckIn) && i.CheckOut.Equals(item.CheckOut));         
                            TempData["Message"] = "This room is Exists in shopping cart";           
                    }
                    else if(Day != null)
                    {
                        cart.Add(item);
                        TempData["Message"] = "Add Room to cart successfully";
                    }
                    else {
                        TempData["Message"] = "Check In and Check Out not same";
                    }
                    HttpContext.Session.SetComplexData("CART", cart);
                }
                else //When cart is null -> create new cart
                {
                    var list = new List<Cart>();
                    list.Add(item);
                    HttpContext.Session.SetComplexData("CART", list);
                    TempData["Message"] = "Add product to cart successfully";
                }

                return RedirectToPage("./Shopping");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return Page();
        }
        
    }
}

#region ExtendSession
public static class SessionExtensions
{
    public static T GetComplexData<T>(this ISession session, string key)
    {
        var data = session.GetString(key);
        if (data == null)
        {
            return default(T);
        }
        return JsonConvert.DeserializeObject<T>(data);
    }

    public static void SetComplexData(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }
}
#endregion