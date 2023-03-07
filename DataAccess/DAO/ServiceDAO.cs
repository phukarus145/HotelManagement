using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.DAO
{
   public class ServiceDAO
    {
        private static ServiceDAO instance = null;
        private static readonly object instanceLock = new object();
        private ServiceDAO() { }
        public static ServiceDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceDAO();
                    }
                    return instance;
                }
            }
        }
        public static List<Service> GetServices()
        {
            var services = new List<Service>();

            try
            {
                using (var context = new HotelManagementContext())
                {
                    services = context.Service.ToList();
                }
                return services;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static void AddService(Service m)
        {
            try
            {


                using (var context = new HotelManagementContext())
                {
                    var p1 = context.Service.SingleOrDefault(c => c.Title.Equals(m.Title));
                    var p2 = context.Service.SingleOrDefault(c => c.Id.Equals(m.Id));
                    if (p1 == null)
                    {
                        if (p2 == null)
                        {
                            context.Service.Add(m);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Id is Exits");
                        }


                    }
                    else
                    {
                        throw new Exception("Service is Exits");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateService(Service m)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {

                    context.Entry<Service>(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteService(Service p)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {
                    var p1 = context.Service.SingleOrDefault(c => c.Id == p.Id);
                    context.Service.Remove(p1);

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IList<Service> SearchByName(string search)
        {
            IList<Service> searchResult = null;

            using (var context = new HotelManagementContext())
            {
                var searchValues = from service in context.Service
                                   where service.Title.ToLower().Contains(search.ToLower())
                                   select service;

                searchResult = searchValues.ToList();
            }
            return searchResult;
        }
        public Service GetServiceById(int Id)
        {

            IEnumerable<Service> rooms = GetServices();
            Service room = rooms.SingleOrDefault(mb => mb.Id == Id);
            return room;
        }
        public void UpdateActive(int ID, bool? active)
        {

            try
            {

                var service = new Service() { Id = ID, Active = !active };
                using (var db = new HotelManagementContext())
                {
                    db.Service.Attach(service);
                    db.Entry(service).Property(x => x.Active).IsModified = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateQuantity(int ID, int quantity)
        {

            try
            {

                var service = new Service() { Id = ID, InStock = quantity };
                using (var db = new HotelManagementContext())
                {
                    db.Service.Attach(service);
                    db.Entry(service).Property(x => x.InStock).IsModified = true;
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
