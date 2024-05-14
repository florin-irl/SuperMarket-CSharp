using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuperMarket.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object? _currentPage;
        public object? CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public MainViewModel()
        {
            var loginPage = new LoginPage();
            CurrentPage = loginPage;
            //if(loginPage.DataContext is not LoginViewModel loginVm)
            //{
            //    Application.Current.Shutdown();
            //}
            //else
            //{
            //    var username = loginVm.Username;
            //    var password = loginVm.Password;
            //    var userBLL = new UserBLL();
            //    if(userBLL.IsAdmin(username,password))
            //    {
            //        var adminPage = new MainAdminPage();
            //        CurrentPage = adminPage;
            //    }
            //    else
            //        if(userBLL.IsUser(username,password))
            //    {
            //        var userPage = new MainCashierPage();
            //        CurrentPage = userPage;
            //    }
            //}
        }

    }
}
