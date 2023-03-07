using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace DataAccess.Repository
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetMembers();
        Account LoginMember(String email, String password);
        Account GetProfile(int AccountID);
        void ChangePassword(int AccountID, string password);
        void UpdateActive(int AccountID, bool? active);
        void DeleteMember(Account m);
        bool IsValidEmail(string email);
        void UpdateMember(Account m);
        void AddMember(Account m);
        IList<Account> SearchByName(string search);
        IEnumerable<Account> FilterByTeacher(bool isTeacher);
        IEnumerable<Account> FilterByStudent(bool isTeacher);
        IEnumerable<Account> SearchByID(int search);
    }
}
