using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;

namespace DataAccess.Repository
{
    public class RoomRepository : IRoomRepository
    {
        public IEnumerable<Room> GetRooms() => RoomDAO.GetRooms();
        public IList<Room> SearchByNameWithType(string search, int? id) => RoomDAO.Instance.SearchByNameWithType(search, id);
        public Room GetRoomById(int AccountID) => RoomDAO.Instance.GetRoomById(AccountID);
        public void DeleteRoom(Room m) => RoomDAO.DeleteRoom(m);
      
        public void AddRoom(Room m) => RoomDAO.AddRoom(m);
        public void UpdateRoom(Room m) => RoomDAO.UpdateRoom(m);
        public IList<Room> SearchByName(string search) => RoomDAO.Instance.SearchByName(search);
        public IList<Room> FilterByTypeId(int? id) => RoomDAO.Instance.FilterByTypeId(id);
        public void UpdateActive(int ID, bool? active) => RoomDAO.Instance.UpdateActive(ID, active);
     
    }
}
