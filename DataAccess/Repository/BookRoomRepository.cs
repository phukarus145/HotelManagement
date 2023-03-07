using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;

namespace DataAccess.Repository
{
    public class BookRoomRepository : IBookRoomRepository
    {
        public IList<BookRoom> GetBookRooms() => BookRoomDAO.GetBookRooms();
        public IList<BookRoom> GetHistory(int? id) => BookRoomDAO.Instance.GetHistory(id);
        public IList<BookRoom> GetStatus(DateTime CheckIn,DateTime CheckOut) => BookRoomDAO.Instance.StatusRoom(CheckIn, CheckOut);
        public IList<BookRoom> GetListStatusCheckInRoomInDay() => BookRoomDAO.Instance.GetListStatusCheckInRoomInDay();
        public IList<BookRoom> GetListStatusBookedRoomInDay() => BookRoomDAO.Instance.GetListStatusBookedRoomInDay();
        public void AddBookRoom(BookRoom m) => BookRoomDAO.AddBookRoom(m);

        public BookRoom GetBookRoomById(int Id) => BookRoomDAO.Instance.GetBookRoomById(Id);
        public void UpdateStatusFinish(int Id) => BookRoomDAO.Instance.UpdateStatusFinish(Id);
        public void UpdateStatusCheckOut(int Id) => BookRoomDAO.Instance.UpdateStatusCheckOut(Id);
        public void UpdateStatusCancel(int Id) => BookRoomDAO.Instance.UpdateStatusCancel(Id);

        public void UpdateStatusCheckIn(int Id, decimal cmnd) => BookRoomDAO.Instance.UpdateStatusCheckIn(Id,cmnd);
        public void UpdatePrice(int Id, decimal price) => BookRoomDAO.Instance.UpdatePrice(Id, price);
    }

}
