using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Models.EntityLayer;
using SuperMarket.ViewModels.Commands;
using SuperMarket.Views;
using SuperMarket.Views.SpecialQueries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        private ReceiptBLL _receiptBLL = new ReceiptBLL();

        public ICommand GenerateReportCommand { get; set; }

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
            GenerateReportCommand = new RelayCommand<object>(GenerateReport);
        }

        private void GenerateReport(object? obj)
        {
            if (obj is not CashierRevenueMonthPage currentPage)
            {
                return;
            }
            if (string.IsNullOrEmpty(SelectedCashier) || string.IsNullOrEmpty(SelectedMonth))
            {
                return;
            }

            int selectedMonthIndex = Months.IndexOf(SelectedMonth) + 1; // Months is 0-indexed, DateTime.Month is 1-indexed
            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, selectedMonthIndex);

            Revenues.Clear();
            for (int day = 1; day <= daysInMonth; day++)
            {
                Revenues.Add(new DateRevenue
                {
                    Date = new DateTime(DateTime.Now.Year, selectedMonthIndex, day),
                    TotalRevenue = 0
                });
            }
            Decimal Total = 0;
            Cashier = _userBLL.GetAllUsers().FirstOrDefault(u => u.Username == SelectedCashier);
            foreach (DateRevenue revenue in Revenues)
            {
                Total = 0;
                foreach(Receipt receipt in _receiptBLL.GetAllReceipts())
                {
                    if (receipt.UserID == Cashier.UserId && receipt.IssueDate.Day == revenue.Date.Day && receipt.IssueDate.Month == revenue.Date.Month && receipt.IssueDate.Year == revenue.Date.Year)
                    {
                        Total += receipt.TotalPrice;
                    }
                }
                revenue.TotalRevenue = Total;
            }
        }
    }
}
