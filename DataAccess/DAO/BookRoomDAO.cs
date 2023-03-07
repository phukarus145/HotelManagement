using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.DAO
{
   public class BookRoomDAO
    {
        private static BookRoomDAO instance = null;
        private static readonly object instanceLock = new object();
        private BookRoomDAO() { }
        public static BookRoomDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookRoomDAO();
                    }
                    return instance;
                }
            }
        }
        public static List<BookRoom> GetBookRooms()
        {
            var members = new List<BookRoom>();

            try
            {
                using (var context = new HotelManagementContext())
                {
                    members = context.BookRoom.ToList();
                   

                }
                return members;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public BookRoom GetBookRoomById(int Id)
        {

            IEnumerable<BookRoom> members = GetBookRooms();
            BookRoom member = members.SingleOrDefault(mb => mb.Id == Id);
            return member;
        }
        public List<BookRoom> StatusRoom(DateTime CheckIn, DateTime CheckOut)
        {
            List<BookRoom> searchResult = null;
            using (var context = new HotelManagementContext())
            {
                var searchValues = from booking in context.BookRoom
                                   where booking.StarTime >= CheckIn.Date && booking.EndTime < CheckIn.Date || booking.StarTime < CheckOut.Date && booking.EndTime >= CheckOut.Date || booking.StarTime >= CheckIn.Date && booking.EndTime <= CheckOut.Date
                                   select booking;
                searchResult = searchValues.ToList(); 
            }

            return searchResult;
        }
        public List<BookRoom> GetListStatusCheckInRoomInDay()
        {
            DateTime toDay = DateTime.Now;
            
            List<BookRoom> searchResult = null;
            using (var context = new HotelManagementContext())
            {
              if(toDay.Hour >= 12)
                {
                    var searchValues = from booking in context.BookRoom
                                       where booking.StarTime <= toDay.Date && booking.EndTime > toDay.Date
                                       
                                       select booking;
                    searchResult = searchValues.ToList();
                }
                else
                {
                    var searchValues = from booking in context.BookRoom
                                       where booking.StarTime <= toDay.Date && booking.EndTime >= toDay.Date
                                       select booking;
                    searchResult = searchValues.ToList();
                }
               
            }
            return searchResult;
        }
        public List<BookRoom> GetListStatusBookedRoomInDay()
        {
            DateTime toDay = DateTime.Now;
            List<BookRoom> searchResult = null;
            using (var context = new HotelManagementContext())
            {
                if (toDay.Hour < 12)
                {
                    var searchValues = from booking in context.BookRoom
                                       where booking.StarTime == toDay.Date && booking.EndTime >= toDay.Date
                                       select booking;
                    searchResult = searchValues.ToList();
                }
            }
            return searchResult;
        }
        
        public static void AddBookRoom(BookRoom m)
        {
            try
            {
                using (var context = new HotelManagementContext())
                { 
                        context.BookRoom.Add(m);
                        context.SaveChanges();
                }
            }catch (Exception ex)
           {
                throw new Exception(ex.Message);
            }
        }
        public IList<BookRoom> GetHistory(int? id)
        {
            IList<BookRoom> searchResult = null;
            using (var context = new HotelManagementContext())
            {
                var searchValues = from history in context.BookRoom
                                   where history.AccountId == id
                                   select history;
                searchResult = searchValues.ToList();
            }

            return searchResult;
        }
        public void UpdateStatusCheckIn(int Id, decimal cmnd)
        {
        
          try
            {
                List<BookRoom> room1 = GetBookRooms();
                if(room1.Any(x => x.Id == Id && x.Cmnd == cmnd))
                {
                    var room = new BookRoom() { Id = Id, Status = "CHECKIN" };
                    using (var db = new HotelManagementContext())
                    {
                        db.BookRoom.Attach(room);
                        db.Entry(room).Property(x => x.Status).IsModified = true;
                        db.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("CMND Not Correct");
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateStatusCheckOut(int Id)
        {

            try
            {

                var room = new BookRoom() { Id = Id, Status = "CHECKOUT" };
                using (var db = new HotelManagementContext())
                {
                    db.BookRoom.Attach(room);
                    db.Entry(room).Property(x => x.Status).IsModified = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void UpdateStatusFinish(int Id)
        {

            try
            {

                var room = new BookRoom() { Id = Id, Status = "FINISH" };
                using (var db = new HotelManagementContext())
                {
                    db.BookRoom.Attach(room);
                    db.Entry(room).Property(x => x.Status).IsModified = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateStatusCancel(int Id)
        {

            try
            {
                DateTime toDay = DateTime.Now;
                BookRoom day = GetBookRoomById(Id);
                if(day.StarTime > toDay.Date && day.EndTime > toDay.Date)
                {
                    var room = new BookRoom() { Id = Id, Status = "CANCEL" };
                    using (var db = new HotelManagementContext())
                    {
                        db.BookRoom.Attach(room);
                        db.Entry(room).Property(x => x.Status).IsModified = true;
                        db.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Not Cancel");
                }

               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdatePrice(int Id, decimal price)
        {

            try
            {

                var user = new BookRoom() { Id = Id, Total = price };
                using (var db = new HotelManagementContext())
                {
                    db.BookRoom.Attach(user);
                    db.Entry(user).Property(x => x.Total).IsModified = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
