using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;

namespace DataAccess.Repository
{
   public class ServiceRepository : IServiceRepository
    {
        public IEnumerable<Service> GetServices() => ServiceDAO.GetServices();

        public Service GetServiceById(int AccountID) => ServiceDAO.Instance.GetServiceById(AccountID);
        public void DeleteService(Service m) => ServiceDAO.DeleteService(m);

        public void AddService(Service m) => ServiceDAO.AddService(m);
        public void UpdateService(Service m) => ServiceDAO.UpdateService(m);
        public IList<Service> SearchByName(string search) => ServiceDAO.Instance.SearchByName(search);
        public void UpdateActive(int ID, bool? active) => ServiceDAO.Instance.UpdateActive(ID, active);
        public void UpdateQuantity(int ID, int Quantity) => ServiceDAO.Instance.UpdateQuantity(ID, Quantity);
    }
}
