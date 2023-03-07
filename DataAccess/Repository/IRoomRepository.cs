using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.Repository
{
    public interface  IRoomRepository
    {
        IEnumerable<Room> GetRooms();

        Room GetRoomById(int Id);
        
        void DeleteRoom(Room m);
     
        void UpdateRoom(Room m);
        void AddRoom(Room m);
        IList<Room> SearchByName(string search);
        IList<Room> SearchByNameWithType(string search, int? id);
        IList<Room> FilterByTypeId(int? id);
         void UpdateActive(int ID, bool? active);
        
    }
}
