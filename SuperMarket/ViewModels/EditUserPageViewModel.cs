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
using System.Windows.Input;

namespace SuperMarket.ViewModels
{
    public class EditUserPageViewModel : BaseViewModel
    {
        public ICommand AddNewUserCommand { get; set; }
        public bool IsAdmin { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool Add;

        private UserBLL _userBLL = new UserBLL();

        public EditUserPageViewModel()
        {
            AddNewUserCommand = new RelayCommand<object>(SaveNewUser);
            Add = true;
        }

        public void SaveNewUser(object? obj)
        {
            if(obj is not EditUserPage editUserPage)
            {
                return;
            }
            if (Add == false)
            {
                return;
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
