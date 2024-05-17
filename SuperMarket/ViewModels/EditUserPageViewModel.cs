using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Models.EntityLayer;
using SuperMarket.ViewModels.Commands;
using SuperMarket.Views;
using SuperMarket.Views.EditPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SuperMarket.ViewModels
{
    public class EditUserPageViewModel : BaseViewModel
    {
        private bool _isAdmin;
        private string _username;
        private string _password;
        private int _userId;

        public ICommand AddNewUserCommand { get; set; }
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }

        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public int? Id
        {
            get => _userId;
            set
            {
                _userId = value ?? 0;
                OnPropertyChanged(nameof(Id));
            }
        }

        public bool Add;

        private UserBLL _userBLL = new UserBLL();

        public EditUserPageViewModel()
        {
            AddNewUserCommand = new RelayCommand<object>(SaveNewUser);
            Add = true;
        }

        public EditUserPageViewModel(User user)
        {
            AddNewUserCommand = new RelayCommand<object>(SaveNewUser);
            IsAdmin = user.IsAdmin;
            Username = user.Username;
            Password = user.Password;
            Id = user.UserId;
            Add = false;
        }

        public void SaveNewUser(object? obj)
        {
            if(obj is not EditUserPage editUserPage)
            {
                return;
            }
            var users = _userBLL.GetAllUsers();
            var isUnique = (from user in users
                                                       where user.Username == Username
                                                                                  select user).ToList().Count == 0;
            if(isUnique==false) { 
                var warning = MessageBox.Show("Username already exists", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; }
            if (Add == false)
            {
                User user = new User();
                user.IsAdmin = IsAdmin;
                user.Username = Username;
                user.Password = Password;
                user.UserId = Id ?? 0;
                _userBLL.UpdateUser(user);

                var mainAdminPage = new MainAdminPage();
                editUserPage.NavigationService.Navigate(mainAdminPage);
            }
            else
            {
                if (Username == "" || Password == "")
                {
                    return;
                }

                User user = new User();
                user.IsAdmin = IsAdmin;
                user.Username = Username;
                user.Password = Password;
                _userBLL.AddUser(user);

                var mainAdminPage = new MainAdminPage();
                editUserPage.NavigationService.Navigate(mainAdminPage);
            }
           
        }

    }
}
