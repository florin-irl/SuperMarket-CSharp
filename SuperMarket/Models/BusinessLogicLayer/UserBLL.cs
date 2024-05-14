using SuperMarket.Models.DataAccessLayer;
using SuperMarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Models.BusinessLogicLayer
{
    public class UserBLL
    {
        private UserDAL _userDAL = new UserDAL();

        public bool IsAdmin(string username,string password)
        {
            ObservableCollection<User> admins = _userDAL.GetAdmins();
            foreach(User admin in admins)
            {
                if(admin.Username == username && admin.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsUser(string username, string password)
        {
            ObservableCollection<User> users = _userDAL.GetAllUsers();
            foreach (User user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public ObservableCollection<User> GetAllUsers()
        {
            return _userDAL.GetAllUsers();
        }
    }
}
