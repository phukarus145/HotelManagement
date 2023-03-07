using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.Repository
{
    public interface IBookRoomRepository
    {
        IList<BookRoom> GetBookRooms();
        IList<BookRoom> GetHistory(int? id); 
         IList<BookRoom> GetStatus(DateTime CheckIn, DateTime CheckOut);
        IList<BookRoom> GetListStatusCheckInRoomInDay();
        IList<BookRoom> GetListStatusBookedRoomInDay();
        void AddBookRoom(BookRoom m);
        BookRoom GetBookRoomById(int Id);
        void UpdateStatusFinish(int Id);
        void UpdateStatusCheckOut(int Id);
        void UpdateStatusCancel(int Id);
        void UpdateStatusCheckIn(int Id,decimal cmnd);
        void UpdatePrice(int Id, decimal price);
    }
}
