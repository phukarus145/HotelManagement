using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;

namespace DataAccess.Repository
{
    public interface IRoomInBookingRepository
    {
        IList<Room> GetRoomInBooking(int Id);
        IList<RoomInBooking> GetRoomInBookingByRoom(int Id);
        void AddRoominBooking(RoomInBooking m);
        IEnumerable<RoomInBooking> GetRooms();
    }
}
