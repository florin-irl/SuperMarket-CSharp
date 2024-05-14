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
        }

    }
}
