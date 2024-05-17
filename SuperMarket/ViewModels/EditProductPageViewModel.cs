using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Models.EntityLayer;
using SuperMarket.ViewModels.Commands;
using SuperMarket.Views;
using SuperMarket.Views.EditPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace SuperMarket.ViewModels
{
    public class EditProductPageViewModel : BaseViewModel
    {
        private int? _id;
        private string _name;
        private string _barcode;
        private int _categoryId;
        private int _producerId;

        public int? Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Barcode
        {
            get => _barcode;
            set
            {
                _barcode = value;
                OnPropertyChanged(nameof(Barcode));
            }
        }
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
            }
        }
        public int ProducerId
        {
            get => _producerId;
            set
            {
                _producerId = value;
                OnPropertyChanged(nameof(ProducerId));
            }
        }
        private ProductBLL _productBLL { get; set; } = new ProductBLL();
        private CategoryBLL _categoryBLL { get; set; } = new CategoryBLL();
        private ProducerBLL _producerBLL { get; set; } = new ProducerBLL();

        public ObservableCollection<string> Categories { get; set; }
        public ObservableCollection<string> Producers { get; set; }

        public string CategoryName { get; set; }
        public string ProducerName { get; set; }

        public ICommand SaveCommand { get; set; }

        public bool Add;

        public EditProductPageViewModel()
        {
            SaveCommand = new RelayCommand<object>(Save);
            ObservableCollection<Category> categories = _categoryBLL.GetAllCategories();
            Categories = new ObservableCollection<string>();
            foreach (var category in categories)
            {
                Categories.Add(category.Name);
            }
            ObservableCollection<Producer> producers = _producerBLL.GetAllProducers();
            Producers = new ObservableCollection<string>();
            foreach (var producer in producers)
            {
                Producers.Add(producer.Name);
            }
            Add = true;
        }

        public EditProductPageViewModel(Product product)
        {
            SaveCommand = new RelayCommand<object>(Save);
            Id = product.ProductId;
            Name = product.Name;
            Barcode = product.Barcode;
            CategoryId = product.CategoryId;
            ProducerId = product.ProducerId;
            CategoryName = (from c in _categoryBLL.GetAllCategories() where c.CategoryId == CategoryId select c.Name).FirstOrDefault();
            ProducerName = (from p in _producerBLL.GetAllProducers() where p.ProducerId == ProducerId select p.Name).FirstOrDefault();
            ObservableCollection<Category> categories = _categoryBLL.GetAllCategories();
            Categories = new ObservableCollection<string>();
            foreach (var category in categories)
            {
                Categories.Add(category.Name);
            }
            ObservableCollection<Producer> producers = _producerBLL.GetAllProducers();
            Producers = new ObservableCollection<string>();
            foreach (var producer in producers)
            {
                Producers.Add(producer.Name);
            }
            Add = false;
        }

        public void Save(object? obj)
        {
            if (obj is not EditProductPage editProductPage)
            {
                return;
            }
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Barcode) || string.IsNullOrEmpty(CategoryName) || string.IsNullOrEmpty(ProducerName)
                ||Barcode.Length!=13)
            {
               var warning = MessageBox.Show("You have incomplete or invalid input!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var categories = _categoryBLL.GetAllCategories();
            var producers = _producerBLL.GetAllProducers();
            var products = _productBLL.GetAllProducts();
            var isUnique = !products.Any(product => product.Barcode == Barcode && product.ProductId != Id);
            if (isUnique == false)
            {
                var warning = MessageBox.Show("Barcode already exists", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Add)
            {
                Product product = new Product
                {
                    ProductId = Id ?? 0,
                    Name = Name,
                    Barcode = Barcode,
                    CategoryId = (from c in categories where c.Name == CategoryName select c.CategoryId).FirstOrDefault() ?? 0,
                    ProducerId = (from p in producers where p.Name == ProducerName select p.ProducerId).FirstOrDefault() ?? 0
                };
                _productBLL.AddProduct(product);
                var mainAdminPage = new MainAdminPage();
                editProductPage.NavigationService.Navigate(mainAdminPage);
            }
            else
            {
                Product product = new Product
                {
                    ProductId = Id ?? 0,
                    Name = Name,
                    Barcode = Barcode,
                    CategoryId = (from c in categories where c.Name == CategoryName select c.CategoryId).FirstOrDefault() ?? 0,
                    ProducerId = (from p in producers where p.Name == ProducerName select p.ProducerId).FirstOrDefault() ?? 0
                };
                _productBLL.UpdateProduct(product);
                var mainAdminPage = new MainAdminPage();
                editProductPage.NavigationService.Navigate(mainAdminPage);
            }
        }
    }

    


}
