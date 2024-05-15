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
    public class EditCategoryPageViewModel : BaseViewModel
    {
        private string _name;
        private int? _id;

        public bool Add;

        private CategoryBLL _categoryBLL = new CategoryBLL();

        public ICommand SaveCommand { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int? Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public EditCategoryPageViewModel()
        {
            SaveCommand = new RelayCommand<object>(SaveCategory);
            Add = true;
        }

        public EditCategoryPageViewModel(Category category)
        {
            SaveCommand = new RelayCommand<object>(SaveCategory);
            Add = false;
            Id = category.CategoryId;
            Name = category.Name;
        }

        private void SaveCategory(object obj)
        {
            if (obj is not EditCategoryPage editProducersPage)
            {
                return;
            }
            if(Add==false)
            {
                if(Name =="")
                {
                    return;
                }

                Category category = new Category();
                category.CategoryId = Id;
                category.Name = Name;
                _categoryBLL.UpdateCategory(category);

                var mainAdminPage = new MainAdminPage();
                editProducersPage.NavigationService.Navigate(mainAdminPage);
            }
            else
            {
                if(Name=="")
                {
                    return;
                }
                Category category = new Category();
                category.Name = Name;
                _categoryBLL.AddCategory(category);

                var mainAdminPage = new MainAdminPage();
                editProducersPage.NavigationService.Navigate(mainAdminPage);
            }
        }
    }
}
