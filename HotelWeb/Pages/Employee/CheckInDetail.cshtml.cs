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

namespace HotelWeb.Pages.Employee
{
    public class CheckInDetailModel : PageModel
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IServiceTypeRepository serviceTypeRepository;
        private readonly IRoomRepository roomRepository;
        private readonly IRoomTypeRepository roomTypeRepository;
        private readonly IBookRoomRepository bookRoomRepository;
        private readonly IRoomInBookingRepository roomInBookingRepository;
        private readonly IServiceInRoomRepository serviceInRoomRepository;
        private readonly IAccountRepository accountRepository;


        public CheckInDetailModel(IAccountRepository _accountRepository, IServiceInRoomRepository _serviceInRoomRepository, IRoomRepository _roomRepository, IRoomTypeRepository _roomTypeRepository, IBookRoomRepository _bookRoomRepository, IRoomInBookingRepository _roomInBookingRepository,IServiceRepository _serviceRepository, IServiceTypeRepository _serviceTypeRepository)
        {
            serviceRepository = _serviceRepository;
            serviceTypeRepository = _serviceTypeRepository;
                roomRepository = _roomRepository;
                roomTypeRepository = _roomTypeRepository;
                bookRoomRepository = _bookRoomRepository;
             roomInBookingRepository = _roomInBookingRepository;
            serviceInRoomRepository = _serviceInRoomRepository;
            accountRepository = _accountRepository;
        }
        [BindProperty]
        public Service Service { get; set; }
        [BindProperty]
        public Account Account { get; set; }
        [BindProperty]
        public BookRoom BookRoom { get; set; }
        [BindProperty]
        
        public ServiceInRoom ServiceInRoom { get; set; }
        [BindProperty]
        public ServiceCart ServiceCart { get; set; }
        public IList<Room> RoomList { get; set; }
        public IList<RoomType> RoomTypeList { get; set; }
        public IList<ServiceCart> ServiceCartList { get; set; }
        public IList<Service> ServiceList { get; set; }
        public IList<Room> RoomAllList { get; set; }
        public IList<Account> AccountList { get; set; }
        public IList<Service> ServiceListInRoom { get; set; }
        public IList<ServiceInRoom> ServiceInRoomList { get; set; }
        
        public IList<ServiceType> ServiceTypeList { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "InStock can not be empty")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Wrong format for InStock number")]
        public int? QuantityInCart { get; set; }
        public TimeSpan ts;
        public IList<BookRoom> RoomBookingList { get; set; }
        public IList<Room> RoomNotEmpty { get; set; }
        public List<Room> first = new List<Room>();
        public IActionResult OnGet(int id)
        {
          
            try
            {
               
                AccountList = accountRepository.GetMembers().ToList();
                ServiceInRoomList = serviceInRoomRepository.GetServiecInRooms().ToList();
                RoomTypeList = roomTypeRepository.GetRoomsType().ToList();
                RoomList = roomInBookingRepository.GetRoomInBooking(id);
                BookRoom = bookRoomRepository.GetBookRoomById(id);
                ServiceListInRoom = serviceInRoomRepository.GetServiceInBooking(id);
                ServiceList = serviceRepository.GetServices().Where(x => x.Active == true && x.InStock != 0).ToList();
                ServiceTypeList = serviceTypeRepository.GetServicesType().ToList();
                Account = AccountList.SingleOrDefault(x => x.Id == BookRoom.AccountId);
                RoomAllList = roomRepository.GetRooms().ToList().Where(p => p.Active == true ).ToList();;
                DateTime CheckIn = BookRoom.StarTime ?? default(DateTime);
                DateTime CheckOut = BookRoom.EndTime ?? default(DateTime);
                RoomBookingList = bookRoomRepository.GetStatus(CheckIn, CheckOut).Where(x => x.Status == "BOOKED" || x.Status == "CHECKIN" || x.Status == "CHECKOUT").ToList();
                ts = CheckOut.Subtract(CheckIn);

                foreach (var item in RoomBookingList)
                {
                    RoomNotEmpty = roomInBookingRepository.GetRoomInBooking(item.Id);
                    first.AddRange(RoomNotEmpty);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }

            return Page();
        }
      public IActionResult OnPostAddService(int id, int QuantityInCart, int bookid)
        {
           
            try
            {
                BookRoom bookRoom =   bookRoomRepository.GetBookRoomById(bookid);
                Service service = serviceRepository.GetServiceById(id);
                IList<Service> a = serviceInRoomRepository.GetServiceInBooking(bookid);
                if(QuantityInCart > 0 && QuantityInCart <= service.InStock)
                {
                    if (a.Any(x => x.Id == service.Id) == false)
                    {
                        ServiceInRoom item = new ServiceInRoom
                        {
                            BookRoomId = bookid,
                            ServiceId = id,
                            Quantity = QuantityInCart
                        };

                        serviceInRoomRepository.AddServiceinBooking(item);
                        int quantity = service.InStock ?? default(int);
                        serviceRepository.UpdateQuantity(id, quantity - QuantityInCart);
                        decimal totalCurrent = bookRoom.Total ?? default(decimal);
                        decimal price = service.Amount ?? default(decimal);
                        decimal total = price * QuantityInCart;
                        bookRoomRepository.UpdatePrice(bookid, totalCurrent + total);
                        return RedirectToRoute(new { id = bookid });

                    }
                    else
                    {
                        ServiceInRoom serviceInRoom = serviceInRoomRepository.GetServiceInBookingByQuantity(bookid, id);

                        ServiceInRoom item = new ServiceInRoom
                        {
                            BookRoomId = bookid,
                            ServiceId = id,
                            Quantity = QuantityInCart + serviceInRoom.Quantity
                        };
                        serviceInRoomRepository.UpdateServiceinBooking(item);
                        int quantity = service.InStock ?? default(int);
                        serviceRepository.UpdateQuantity(id, quantity - QuantityInCart);
                        decimal totalCurrent = bookRoom.Total ?? default(decimal);
                        decimal price = service.Amount ?? default(decimal);
                        decimal total = price * QuantityInCart;
                        bookRoomRepository.UpdatePrice(bookid, totalCurrent + total);
                        return RedirectToRoute(new { id = bookid });
                    }
                }
                else
                {
                    TempData["Message"] =("Quantity not greater Instock");
                    return RedirectToRoute(new { id = bookid });
                }
                
             
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToRoute(new { id = bookid });

            }
          
        }
        public IActionResult OnPostPayment(int id)
        {
            try
            {
                bookRoomRepository.UpdateStatusCheckOut(id);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return Page();

            }
          

            
        }
        public IActionResult OnPostAddRoom(int id, int bookid)
        {
            try
            {
               
                BookRoom bookRoom = bookRoomRepository.GetBookRoomById(bookid);
                Room room = roomRepository.GetRoomById(id);
                RoomInBooking roominbooking = new RoomInBooking
                {
                    BookRoomId = bookid,
                    RoomId = id
                };
                roomInBookingRepository.AddRoominBooking(roominbooking);
                DateTime CheckIn = bookRoom.StarTime ?? default(DateTime);
                DateTime CheckOut = bookRoom.EndTime ?? default(DateTime);
                ts = CheckOut.Subtract(CheckIn);
                decimal totalCurrent = bookRoom.Total ?? default(decimal);
                decimal price = room.Amount ?? default(decimal);
                bookRoomRepository.UpdatePrice(bookid, decimal.Parse(ts.Days.ToString()) * price + totalCurrent);

                return RedirectToRoute(new { id = bookid });
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToRoute(new { id = bookid });

            }



        }

    }
}
