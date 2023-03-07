using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;


namespace DataAccess.DAO
{
    public  class RoomDAO
    {
        private static RoomDAO instance = null;
        private static readonly object instanceLock = new object();
         private RoomDAO() { }
        public static RoomDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomDAO();
                    }
                    return instance;
                }
            }
        }
        public static List<Room> GetRooms()
        {
            var rooms = new List<Room>();

            try
            {
                using (var context = new HotelManagementContext())
                {
                    rooms = context.Room.ToList();
                }
                return rooms;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void AddRoom(Room m)
        {
            try
            {


                using (var context = new HotelManagementContext())
                {
                    var p1 = context.Room.SingleOrDefault(c => c.Title.Equals(m.Title));
                    var p2 = context.Room.SingleOrDefault(c => c.Id.Equals(m.Id));
                    if (p1 == null)
                    {
                        if(p2 == null)
                        {
                            context.Room.Add(m);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Id is Exits");
                        }
                    }
                    else
                    {
                        throw new Exception("Room is Exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateRoom(Room m)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {

                    context.Entry<Room>(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteRoom(Room p)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {
                    var p1 = context.Room.SingleOrDefault(c => c.Id == p.Id);
                    context.Room.Remove(p1);

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IList<Room> SearchByName(string search)
        {
            IList<Room> searchResult = null;

            using (var context = new HotelManagementContext())
            {
                var searchValues = from room in context.Room
                                   where room.Title.ToLower().Contains(search.ToLower())  
                                   select room;

                searchResult = searchValues.ToList();
            }
            return searchResult;
        }
        public IList<Room> SearchByNameWithType(string search, int? id)
        {
            IList<Room> searchResult = null;

            using (var context = new HotelManagementContext())
            {
                var searchValues = from room in context.Room
                                   where room.Title.ToLower().Contains(search.ToLower()) && room.RoomTypeId.Equals(id)
                                   select room;

                searchResult = searchValues.ToList();
            }
            return searchResult;
        }
        public Room GetRoomById(int Id)
        {

            IEnumerable<Room> rooms = GetRooms();
            Room room = rooms.SingleOrDefault(mb => mb.Id == Id);
            return room;
        }
        public void UpdateActive(int ID, bool? acticve)
        {

            try
            {
                

                var room = new Room() { Id = ID, Active = !acticve };
                using (var db = new HotelManagementContext())
                {
                    db.Room.Attach(room);
                    db.Entry(room).Property(x => x.Active).IsModified = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        public IList<Room> FilterByTypeId(int? id)
        {
            IList<Room> searchResult = null;
            using (var context = new HotelManagementContext())
            {
                var searchValues = from room in context.Room
                                   where room.RoomTypeId == id
                                   select room;
                searchResult = searchValues.ToList();
            }

            return searchResult;
        }
    }
}
