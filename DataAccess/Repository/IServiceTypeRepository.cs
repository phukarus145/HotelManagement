using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.Repository
{
    public interface IServiceTypeRepository
    {
        IEnumerable<ServiceType> GetServicesType();

        ServiceType GetServiceTypeById(int Id);

        void DeleteServiceType(ServiceType m);

        void UpdateServiceType(ServiceType m);
        void AddServiceType(ServiceType m);
        IList<ServiceType> SearchByName(string search);
        void UpdateActive(int ID, bool? active);
    }
}
