using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;

namespace DataAccess.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public IEnumerable<RoomType> GetRoomsType() => RoomTypeDAO.GetRoomsType();

        public RoomType GetRoomTypebyId(int Id) => RoomTypeDAO.Instance.GetRoomTypeById(Id);
        public void DeleteRoomType(RoomType m) => RoomTypeDAO.DeleteRoomType(m);

        public void AddRoomType(RoomType m) => RoomTypeDAO.AddRoomType(m);
        public void UpdateRoomType(RoomType m) => RoomTypeDAO.UpdateRoomType(m);
        public IList<RoomType> SearchByName(string search) => RoomTypeDAO.Instance.SearchByName(search);
    }
}
