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

namespace HotelWeb.Pages.Booking
{
    public class CartModel : PageModel
    {
        private readonly IRoomRepository roomRepository;
        private readonly IRoomTypeRepository roomTypeRepository;
        private readonly IBookRoomRepository bookRoomRepository;
        private readonly ICouponRepository couponRepository;
        private readonly IRoomInBookingRepository roomInBookingRepository;
        public CartModel(IRoomRepository _roomRepository, IRoomTypeRepository _roomTypeRepository, IBookRoomRepository _bookRoomRepository, ICouponRepository _couponRepository, IRoomInBookingRepository _roomInBookingRepository)
        {
            roomRepository = _roomRepository;
            roomTypeRepository = _roomTypeRepository;
            bookRoomRepository = _bookRoomRepository;
            couponRepository = _couponRepository;
            roomInBookingRepository = _roomInBookingRepository;
        }
        public IList<Cart> Cart { get; set; }
        [BindProperty]
        public Cart CartItem { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Cmnd can not be empty")]
        [RegularExpression("[0-9]{9,12}", ErrorMessage = "Wrong format for Cmnd number")]
        public decimal? Cmnd { get; set; }
        public int? discount { get; set; }
        public decimal? TotalPrice { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "CheckIn can not be empty")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }
        [BindProperty]
        public BookRoom BookRoom { get; set; }
        [BindProperty]
        public Coupon Coupon { get; set; }


        [BindProperty]
        [Required(ErrorMessage = "CheckOut can not be empty")]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
        private decimal? GetTotalPrice(List<Cart> cart)
        {
            decimal? result = 0;
            foreach (Cart item in cart)
            {
                result += item.Amount;
            }
            return result;
        }
        private decimal? GetTotalPriceDiscount(List<Cart> cart, int? discound)
        {
            decimal? result = 0;
            foreach (Cart item in cart)
            {
                result += item.Amount;
            }
            return (result*discound)/100;
        }
        public IActionResult OnGet()
        {
            try
            {
           
                var cart = HttpContext.Session.GetComplexData<List<Cart>>("CART");
                var day = cart.Take(1).ToList();
                CartItem = cart.FirstOrDefault(x => x.RoomId != null);
                if (cart != null)
                {
                    foreach (var day1 in day)
                    {
                        CheckIn = day1.CheckIn;
                        CheckOut = day1.CheckOut;
                    }
                    TotalPrice = GetTotalPrice(cart);
                    Cart = cart;
                }
                return Page();
            }
            catch (Exception ex)
            {
             
            }
            return Page();
        }
        public IActionResult OnAddtoCart()
        {

            try
            {


            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            return Page();
        }
        public IActionResult OnPostDelete(int Id)
        {
            //if (HttpContext.Session.GetString("EMAIL") == null)
            //{
            //    return RedirectToPage("/Login");
            //}


            var cart = HttpContext.Session.GetComplexData<List<Cart>>("CART");
            if (cart != null)
            {
                if (cart.Exists(i => i.RoomId == Id))
                {
                    var item = cart.FirstOrDefault(i => i.RoomId == Id);
                    cart.Remove(item);
                    if (cart.Count > 0)
                    {
                        HttpContext.Session.SetComplexData("CART", cart);
                    }
                    else
                    {
                        HttpContext.Session.Remove("CART");
                    }
                }
            }
            return RedirectToPage();
        }
        public IActionResult OnPostPayment()
        {
            var cart = HttpContext.Session.GetComplexData<List<Cart>>("CART");
            CartItem = cart.FirstOrDefault(x => x.RoomId != null);
            if (HttpContext.Session.GetString("EMAIL") == null)
            {
                return RedirectToPage("/Login");
            }
            if (Coupon.Id == null)
            {
         
                BookRoom bookroom = new BookRoom
                {
                    CouponId = Coupon.Id,
                    StarTime = CartItem.CheckIn.Date,
                    EndTime = CartItem.CheckOut.Date,
                    AccountId = int.Parse(HttpContext.Session.GetString("Id")),
                    Cmnd = BookRoom.Cmnd,
                    Status = "BOOKED",
                    Total =  GetTotalPrice(cart)
            };
                bookRoomRepository.AddBookRoom(bookroom);
                List<BookRoom> BookRoomList = bookRoomRepository.GetBookRooms().ToList();
                BookRoom newItem = BookRoomList.LastOrDefault(x => x.Id != null);
                foreach (var item in cart)
                {
                    RoomInBooking roominbooking = new RoomInBooking
                    {
                        BookRoomId = newItem.Id,
                        RoomId = item.RoomId
                    };
                    roomInBookingRepository.AddRoominBooking(roominbooking);
                  
                }
                HttpContext.Session.Remove("CART");
                TempData["Message"] = "Payment successfully";
                return RedirectToPage("./Shopping");
            } 

            else
            {
                IEnumerable<Coupon> coupons = couponRepository.GetCoupons();
                Coupon coupon = coupons.SingleOrDefault(mb => mb.Id.Equals(Coupon.Id));
                if (coupon != null)
                {
                    Coupon = couponRepository.GetCouponById(Coupon.Id);
                    BookRoom bookroom = new BookRoom
                    {
                        CouponId = Coupon.Id,
                        StarTime = CartItem.CheckIn.Date,
                        EndTime = CartItem.CheckOut.Date,
                        AccountId = int.Parse(HttpContext.Session.GetString("Id")),
                        Cmnd = BookRoom.Cmnd,
                        Status = "BOOKED",
                        Total = GetTotalPrice(cart) - GetTotalPriceDiscount(cart, coupon.Discount)
                    };
                    bookRoomRepository.AddBookRoom(bookroom);
                    List<BookRoom> BookRoomList = bookRoomRepository.GetBookRooms().ToList();
                    BookRoom newItem = BookRoomList.LastOrDefault(x => x.Id != null);
                    foreach (var item in cart)
                    {
                        RoomInBooking roominbooking = new RoomInBooking
                        {
                            BookRoomId = newItem.Id,
                            RoomId = item.RoomId
                        };
                        roomInBookingRepository.AddRoominBooking(roominbooking);
                        
                    }
                    HttpContext.Session.Remove("CART");
                    TempData["Message"] = "Payment successfully";
                    return RedirectToPage("./Shopping");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Coupon not Exists";
                    
                    var day = cart.Take(1).ToList();
                  
                    if (cart != null)
                    {
                        foreach (var day1 in day)
                        {
                            CheckIn = day1.CheckIn;
                            CheckOut = day1.CheckOut;
                        }
                        TotalPrice = GetTotalPrice(cart);
                        Cart = cart;
                    }
                    return Page();
                }
            }
            
        

            return RedirectToPage();
        }
    }
}
