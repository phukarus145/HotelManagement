using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess.DAO;

namespace DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public IEnumerable<Account> GetMembers() => AccountDAO.GetMembers();
        public Account LoginMember(string email, string password) => AccountDAO.Instance.Login(email, password);
        public Account GetProfile(int AccountID) => AccountDAO.Instance.GetProfile(AccountID);
        public void DeleteMember(Account m) => AccountDAO.DeleteAccount(m);
        public void ChangePassword(int AccountID, string password) => AccountDAO.Instance.ChangePassword(AccountID, password);
        public void UpdateActive(int AccountID, bool? active) => AccountDAO.Instance.UpdateActive(AccountID, active);
        public void AddMember(Account m) => AccountDAO.AddAccount(m);
        public void UpdateMember(Account m) => AccountDAO.UpdateAccount(m);
        public IList<Account> SearchByName(string search) => AccountDAO.Instance.SearchByName(search);
        public IEnumerable<Account> FilterByTeacher(bool isTeacher) => AccountDAO.Instance.FilterByEmployee(isTeacher);
        public IEnumerable<Account> FilterByStudent(bool isTeacher) => AccountDAO.Instance.FilterByCustomer(isTeacher);
        public IEnumerable<Account> SearchByID(int search) => AccountDAO.Instance.SearchByID(search);
        public bool IsValidEmail(String email) => AccountDAO.Instance.IsValidEmail(email);
    }
}
