using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;

namespace DataAccess.Repository
{
    public class RoomInBookingRepository : IRoomInBookingRepository
    {
        public IList<Room> GetRoomInBooking(int Id) => RoomInBookingDAO.GetRoomInBooking(Id);
        public IList<RoomInBooking> GetRoomInBookingByRoom(int Id) => RoomInBookingDAO.GetRoomInBookingByRoom(Id);
        public void AddRoominBooking(RoomInBooking m) => RoomInBookingDAO.Instance.AddRoomInBooking(m);
        public IEnumerable<RoomInBooking> GetRooms() => RoomInBookingDAO.GetRoomInBookings();
    }
}
