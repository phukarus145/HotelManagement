using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;
        private static readonly object instanceLock = new object();
        private AccountDAO() { }
        public static AccountDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AccountDAO();
                    }
                    return instance;
                }
            }
        }
        public static List<Account> GetMembers()
        {
            var members = new List<Account>();

            try
            {
                using (var context = new HotelManagementContext())
                {
                    members = context.Account.ToList();
                    members.Sort(delegate (Account x, Account y)
                    {
                        if (x.Fullname == null && y.Fullname == null) return 0;
                        else if (x.Fullname == null) return -1;
                        else if (y.Fullname == null) return 1;
                        else return x.Fullname.CompareTo(y.Fullname);
                    });

                }
                return members;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
          
        }
        public Account Login(string email, string password)
        {
     
            IEnumerable<Account> members = GetMembers();
            Account member = members.SingleOrDefault(mb => mb.Email.Equals(email) && mb.Password.Equals(password));
            return member;
        }
        public static void AddAccount(Account m)
        {
            try
            {


                using (var context = new HotelManagementContext())
                {
                    var p1 = context.Account.SingleOrDefault(c => c.Email.Equals(m.Email));
                    var p2 = context.Account.SingleOrDefault(c => c.Id.Equals(m.Id));
                    if (p1 == null)
                    {
                        if (p2 == null)
                        {
                            context.Account.Add(m);
                        context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Id is Exits");
                        }
                    }
                    else
                    {
                        throw new Exception("Email is Exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateAccount(Account m)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {

                    context.Entry<Account>(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteAccount(Account p)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {
                    var p1 = context.Account.SingleOrDefault(c => c.Id == p.Id);
                    context.Account.Remove(p1);

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<Account> FilterByEmployee(bool isTeacher)
        {
            IEnumerable<Account> searchResult = null;
            using (var context = new HotelManagementContext())
            {
                var searchValues = from member in context.Account
                                   where member.IsEmployee == isTeacher
                                   select member;
                searchResult = searchValues.ToList();
            }

            return searchResult;
        }
        public IEnumerable<Account> FilterByCustomer(bool isTeacher)
        {
            IEnumerable<Account> searchResult = null;
            using (var context = new HotelManagementContext())
            {
                var searchValues = from member in context.Account
                                   where member.IsEmployee == isTeacher
                                   select member;
                searchResult = searchValues.ToList();
            }

            return searchResult;
        }
        public IList<Account> SearchByName(string search)
        {
            IList<Account> searchResult = null;

            using (var context = new HotelManagementContext())
            {
                var searchValues = from member in context.Account
                                   where member.Fullname.ToLower().Contains(search.ToLower())
                                   select member;

                searchResult = searchValues.ToList();
            }
            return searchResult;
        }
        public IEnumerable<Account> SearchByID(int search)
        {
            IEnumerable<Account> searchResult = null;
            using (var context = new HotelManagementContext())
            {
                var searchValues = from member in context.Account
                                   where member.Id == search
                                   select member;
                searchResult = searchValues.ToList();
            }
            return searchResult;
        }
        public  Account GetProfile(int AccountID)
        {

            IEnumerable<Account> members = GetMembers();
            Account member =  members.SingleOrDefault(mb => mb.Id == AccountID);
            return member;
        }
        public void ChangePassword(int AccountID, string password)
        {
            try
            {
                var user = new Account() { Id = AccountID, Password = password };
                using (var db = new HotelManagementContext())
                {
                    db.Account.Attach(user);
                    db.Entry(user).Property(x => x.Password).IsModified = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateActive(int AccountID, bool? acticve)
        {
            
            try
            {
         
                var user = new Account() { Id = AccountID, Active = !acticve };
                using (var db = new HotelManagementContext())
                {
                    db.Account.Attach(user);
                    db.Entry(user).Property(x => x.Active).IsModified = true;
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
