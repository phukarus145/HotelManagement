using BusinessObject.Model;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
   public class ServiceInRoomRepository : IServiceInRoomRepository
    {
        public IList<Service> GetServiceInBooking(int Id) => ServiceInRoomDAO.GetServiceInBooking(Id);
        public ServiceInRoom GetServiceInBookingByQuantity(int Id, int serviceId) => ServiceInRoomDAO.GetServiceInBookingByQuantity(Id, serviceId);
        public void AddServiceinBooking(ServiceInRoom m) => ServiceInRoomDAO.Instance.AddServiceInBooking(m);
        public void UpdateServiceinBooking(ServiceInRoom m) => ServiceInRoomDAO.Instance.UpdateServiceInBooking(m);
        public IEnumerable<ServiceInRoom> GetServiecInRooms() => ServiceInRoomDAO.GetSerciveInRooms();
    }
}
