using SuperMarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Services
{
    public class UserSession
    {
        private static UserSession? _instance;
        private static readonly object Lock = new object();

        public User? LoggedInUser { get; private set; }

        private UserSession() { }

        public static UserSession Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ??= new UserSession();
                }
            }
        }

        public void SetUser(User user)
        {
            LoggedInUser = user;
        }

        public void ClearUser()
        {
            LoggedInUser = null;
        }
    }
}
