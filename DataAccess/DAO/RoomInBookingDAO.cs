using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.DAO
{
    public class RoomInBookingDAO
    {
        private static RoomInBookingDAO instance = null;
        private static readonly object instanceLock = new object();
        private RoomInBookingDAO() { }
        public static RoomInBookingDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomInBookingDAO();
                    }
                    return instance;
                }
            }
        }
        public static List<RoomInBooking> GetRoomInBookings()
        {
            var members = new List<RoomInBooking>();

            try
            {
                using (var context = new HotelManagementContext())
                {
                    members = context.RoomInBooking.ToList();
                }
                return members;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static List<Room> GetRoomInBooking(int Id)
        {
            var searchResult = new List<RoomInBooking>();
            List<Room> searchResult1 = null;
           
            // IEnumerable<MemberObject> searchResult = null;
            try
            {
                using (var context = new HotelManagementContext())
                {

                    var searchValues = from booking in context.Room
                                       where booking.RoomInBooking.Any(c => c.BookRoomId == Id)
                                       select booking;

                    searchResult1 = searchValues.ToList();
                }

                return searchResult1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static List<RoomInBooking> GetRoomInBookingByRoom(int Id)
        {
            var searchResult = new List<RoomInBooking>();
            List<RoomInBooking> searchResult1 = null;

            // IEnumerable<MemberObject> searchResult = null;
            try
            {
                using (var context = new HotelManagementContext())
                {

                    var searchValues = from booking in context.RoomInBooking
                                       where booking.BookRoomId == Id
                                       select booking;

                    searchResult1 = searchValues.ToList();
                }

                return searchResult1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public void AddRoomInBooking(RoomInBooking c)
        {
            try
            {

                using (var context = new HotelManagementContext())
                {
                    context.RoomInBooking.Add(c);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
