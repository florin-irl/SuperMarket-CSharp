using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.ViewModels
{
    public class CashierRevenuePageViewModel : BaseViewModel
    {
        public ObservableCollection<string> Cashiers { get; set; }
        public ObservableCollection<string> Months { get;
            set ; }
        public ObservableCollection<DateRevenue> Revenues { get; set; }

        public string SelectedCashier { get; set; }
        public string SelectedMonth { get; set; }
        public User Cashier { get; set; }
        public int IntMonth { get; set; }

        private UserBLL _userBLL = new UserBLL();

        public CashierRevenuePageViewModel()
        {
            Cashiers = new ObservableCollection<string>();
            Months =
            [
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December",
            ];
            Revenues = new ObservableCollection<DateRevenue>();
            Cashier = new User();
            SelectedCashier = "";
            SelectedMonth = "";
            IntMonth = 0;
            ObservableCollection<User> users = new ObservableCollection<User>();
            users = _userBLL.GetAllUsers();
            foreach (User user in users)
            {
                if (user.IsAdmin == false)
                {
                    Cashiers.Add(user.Username);
                }
            }
        }
    }
}
