using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.Repository
{
    public interface IRoomTypeRepository
    {
        IEnumerable<RoomType> GetRoomsType();

        RoomType GetRoomTypebyId(int Id);

        void DeleteRoomType(RoomType m);

        void UpdateRoomType(RoomType m);
        void AddRoomType(RoomType m);
        IList<RoomType> SearchByName(string search);
    }
}
