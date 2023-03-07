using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.DAO
{
    public class ServiceInRoomDAO
    {
        private static ServiceInRoomDAO instance = null;
        private static readonly object instanceLock = new object();
        private ServiceInRoomDAO() { }
        public static ServiceInRoomDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceInRoomDAO();
                    }
                    return instance;
                }
            }
        }
        public static List<ServiceInRoom> GetSerciveInRooms()
        {
            var members = new List<ServiceInRoom>();

            try
            {
                using (var context = new HotelManagementContext())
                {
                    members = context.ServiceInRoom.ToList();
                }
                return members;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static List<Service> GetServiceInBooking(int Id)
        {
            var searchResult = new List<ServiceInRoom>();
            List<Service> searchResult1 = null;

            // IEnumerable<MemberObject> searchResult = null;
            try
            {
                using (var context = new HotelManagementContext())
                {

                    var searchValues = from booking in context.Service
                                       where booking.ServiceInRoom.Any(c => c.BookRoomId == Id)
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
        public void AddServiceInBooking(ServiceInRoom c)
        {
            try
            {

                using (var context = new HotelManagementContext())
                {
                    context.ServiceInRoom.Add(c);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateServiceInBooking(ServiceInRoom c)
        {
            try
            {

                using (var context = new HotelManagementContext())
                {
                    context.Entry<ServiceInRoom>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static ServiceInRoom GetServiceInBookingByQuantity(int Id, int ServiceId)
        {
            
            try
            {
                IEnumerable<ServiceInRoom> serviceInRooms = GetSerciveInRooms();
                ServiceInRoom serviceInRoom = serviceInRooms.SingleOrDefault(mb => mb.BookRoomId == Id && mb.ServiceId == ServiceId);
                return serviceInRoom;

             
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
  
    }
}
