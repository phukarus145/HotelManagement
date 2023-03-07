using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;

namespace DataAccess.Repository
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        public IEnumerable<ServiceType> GetServicesType() => ServiceTypeDAO.GetServicesType();

        public ServiceType GetServiceTypeById(int AccountID) => ServiceTypeDAO.Instance.GetServiceTypeById(AccountID);
        public void DeleteServiceType(ServiceType m) => ServiceTypeDAO.DeleteServiceType(m);

        public void AddServiceType(ServiceType m) => ServiceTypeDAO.AddServiceType(m);
        public void UpdateServiceType(ServiceType m) => ServiceTypeDAO.UpdateServiceType(m);
        public IList<ServiceType> SearchByName(string search) => ServiceTypeDAO.Instance.SearchByName(search);
        public void UpdateActive(int ID, bool? active) => ServiceTypeDAO.Instance.UpdateActive(ID, active);
    }
}
