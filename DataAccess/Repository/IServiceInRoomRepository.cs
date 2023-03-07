using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IServiceInRoomRepository
    {
        IList<Service> GetServiceInBooking(int Id);
        ServiceInRoom GetServiceInBookingByQuantity(int Id, int serviceId); 
        void AddServiceinBooking(ServiceInRoom m);
        void UpdateServiceinBooking(ServiceInRoom m);
        IEnumerable<ServiceInRoom> GetServiecInRooms();
      
    }
}
