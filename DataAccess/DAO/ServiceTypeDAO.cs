using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.DAO
{
   public class ServiceTypeDAO
    {
        private static ServiceTypeDAO instance = null;
        private static readonly object instanceLock = new object();
        private ServiceTypeDAO() { }
        public static ServiceTypeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceTypeDAO();
                    }
                    return instance;
                }
            }
        }
        public static List<ServiceType> GetServicesType()
        {
            var services = new List<ServiceType>();

            try
            {
                using (var context = new HotelManagementContext())
                {
                    services = context.ServiceType.ToList();
                }
                return services;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void AddServiceType(ServiceType m)
        {
            try
            {


                using (var context = new HotelManagementContext())
                {
                    var p1 = context.ServiceType.SingleOrDefault(c => c.Title.Equals(m.Title));
                    var p2 = context.Service.SingleOrDefault(c => c.Id.Equals(m.Id));
                    if (p1 == null)
                    {
                        if (p1 == null)
                        {
                            context.ServiceType.Add(m);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Id is Exist");
                        }

                    }
                    else
                    {
                        throw new Exception("Service is Exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateServiceType(ServiceType m)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {

                    context.Entry<ServiceType>(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteServiceType(ServiceType p)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {
                    var p1 = context.ServiceType.SingleOrDefault(c => c.Id == p.Id);
                    context.ServiceType.Remove(p1);

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IList<ServiceType> SearchByName(string search)
        {
            IList<ServiceType> searchResult = null;

            using (var context = new HotelManagementContext())
            {
                var searchValues = from service in context.ServiceType
                                   where service.Title.ToLower().Contains(search.ToLower())
                                   select service;

                searchResult = searchValues.ToList();
            }
            return searchResult;
        }
        public ServiceType GetServiceTypeById(int Id)
        {

            IEnumerable<ServiceType> rooms = GetServicesType();
            ServiceType room = rooms.SingleOrDefault(mb => mb.Id == Id);
            return room;
        }
        public void UpdateActive(int ID, bool? acticve)
        {

            try
            {

                var service = new ServiceType() { Id = ID, Active = !acticve };
                using (var db = new HotelManagementContext())
                {
                    db.ServiceType.Attach(service);
                    db.Entry(service).Property(x => x.Active).IsModified = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
