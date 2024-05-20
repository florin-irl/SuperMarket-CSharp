using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Services;
using SuperMarket.ViewModels.Commands;
using SuperMarket.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace SuperMarket.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCom { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {
            LoginCom = new RelayCommand<object>(Login);
        }

        private UserBLL _userBLL = new UserBLL();

        public void Login(object parameter)
        {
            if (Username == ""|| Password == "")
            {
                return;
            }


            var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (window == null)
            {
                return;
            }

            var frame = window.FindName("mainFrame") as Frame;
            if (frame == null)
            {
                return;
            }

            var loginPage = frame.Content as LoginPage;
            if (loginPage == null)
            {
                return;
            }

            var usernameTextBox = loginPage.FindName("txtUserName") as TextBox;
            var passwordBox = loginPage.FindName("txtPassword") as PasswordBox;
            Username = usernameTextBox.Text;
            Password = passwordBox.Password;

            if (!_userBLL.IsAdmin(Username, Password)&&!_userBLL.IsUser(Username,Password))
            {
                return;
            }

            if(_userBLL.IsUser(Username,Password))
            {
                UserSession.Instance.SetUser(_userBLL.GetAllUsers().FirstOrDefault(x => x.Username == Username));
                frame.Navigate(new Uri("Views/MainCashierPage.xaml", UriKind.Relative));
            }

            if(_userBLL.IsAdmin(Username,Password))
            {
                frame.Navigate(new Uri("Views/MainAdminPage.xaml", UriKind.Relative));
            }
            
        }
    }
}
