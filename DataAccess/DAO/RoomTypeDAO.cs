using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.DAO
{
    class RoomTypeDAO
    {
        private static RoomTypeDAO instance = null;
        private static readonly object instanceLock = new object();
        private RoomTypeDAO() { }
        public static RoomTypeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomTypeDAO();
                    }
                    return instance;
                }
            }
        }
        public static List<RoomType> GetRoomsType()
        {
            var rooms = new List<RoomType>();

            try
            {
                using (var context = new HotelManagementContext())
                {
                    rooms = context.RoomType.ToList();
                }
                return rooms;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void AddRoomType(RoomType m)
        {
            try
            {


                using (var context = new HotelManagementContext())
                {
                    var p1 = context.RoomType.SingleOrDefault(c => c.Title.Equals(m.Title));
                    var p2 = context.RoomType.SingleOrDefault(c => c.Id.Equals(m.Id));
                    if (p1 == null)
                    {
                        if (p2 == null)
                        {
                            context.RoomType.Add(m);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Id is Exist");
                        }
                    }
                    else
                    {
                        throw new Exception("Type Room is Exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateRoomType(RoomType m)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {

                    context.Entry<RoomType>(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteRoomType(RoomType p)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {
                    var p1 = context.RoomType.SingleOrDefault(c => c.Id == p.Id);
                    context.RoomType.Remove(p1);

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IList<RoomType> SearchByName(string search)
        {
            IList<RoomType> searchResult = null;

            using (var context = new HotelManagementContext())
            {
                var searchValues = from room in context.RoomType
                                   where room.Title.ToLower().Contains(search.ToLower())
                                   select room;

                searchResult = searchValues.ToList();
            }
            return searchResult;
        }
        public RoomType GetRoomTypeById(int Id)
        {

            IEnumerable<RoomType> rooms = GetRoomsType();
            RoomType room = rooms.SingleOrDefault(mb => mb.Id == Id);
            return room;
        }
    }
}
