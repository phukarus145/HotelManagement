using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.Repository
{
    public interface IServiceRepository
    {
        IEnumerable<Service> GetServices();

        Service GetServiceById(int Id);

        void DeleteService(Service m);

        void UpdateService(Service m);
        void AddService(Service m);
        IList<Service> SearchByName(string search);
        void UpdateActive(int ID, bool? active);
        void UpdateQuantity(int ID, int Quantity);
    }
}
