using SuperMarket.Models;
using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Models.DataAccessLayer;
using SuperMarket.Models.EntityLayer;
using SuperMarket.ViewModels.Commands;
using SuperMarket.Views;
using SuperMarket.Views.EditPages;
using SuperMarket.Views.SpecialQueries;
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
    public class MainAdminPageViewModel :BaseViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Stock> Stocks { get; set; }
        public ObservableCollection<Producer> Producers { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Receipt> Receipts { get; set; }

        public ObservableCollection<ProductReceipt> ProductsReceipt { get; set; }

        public ObservableCollection<Offer> Offers { get; set; }

        private UserBLL _userBLL { get; set; } = new UserBLL();
        private StockBLL _stockBLL { get; set; } = new StockBLL();
        private ProducerBLL _producerBLL { get; set; } = new ProducerBLL();

        private CategoryBLL _categoryBLL { get; set; } = new CategoryBLL();
        private ProductBLL _productBLL { get; set; } = new ProductBLL();
        private ReceiptBLL _receiptBLL { get; set; } = new ReceiptBLL();
        private ProductReceiptBLL _productReceiptBLL { get; set; } = new ProductReceiptBLL();
        private OfferBLL _offerBLL { get; set; } = new OfferBLL();


        public ICommand DeleteUserCommand { get; set; }

        public ICommand ModifyUserCommand { get; set; }
        public ICommand AddNewUserCommand { get; set; }

        public ICommand DeleteProducerCommand { get; set; }

        public ICommand ModifyProducerCommand { get; set; }
        public ICommand AddNewProducerCommand { get; set; }

        public ICommand DeleteCategoryCommand { get; set; }
        public ICommand ModifyCategoryCommand { get; set; }
        public ICommand AddNewCategoryCommand { get; set; }

        public ICommand AddNewStockCommand { get; set; }

        public ICommand AddNewProductCommand { get; set; }
        public ICommand ModifyProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }

        public ICommand ViewReceiptInfoCommand { get; set; }

        public ICommand DeleteOfferCommand { get; set; }
        public ICommand AddOfferCommand { get; set; }
        public ICommand ModifyOfferCommand { get; set; }

        public ICommand GoToProducersCategoryListCommand { get; set; }
        public ICommand GoToCategoryValueCommand { get; set; }
        public ICommand GoToUserRevenueMonthlyCommand { get; set; }
        public ICommand GoToHighestValueReceiptCommand { get; set; }

        private object _selectedItem;
        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public MainAdminPageViewModel()
        {
            Users = _userBLL.GetAllUsers();
            Stocks = _stockBLL.GetAllStocks();
            Producers = _producerBLL.GetAllProducers();
            Categories = _categoryBLL.GetAllCategories();
            Products = _productBLL.GetAllProducts();
            Receipts = _receiptBLL.GetAllReceipts();
            ProductsReceipt = _productReceiptBLL.GetAllProductReceipts();
            Offers = _offerBLL.GetAllOffers();

            DeleteUserCommand = new RelayCommand<object>(DeleteUser);
            ModifyUserCommand = new RelayCommand<object>(ModifyUser);
            AddNewUserCommand = new RelayCommand<object>(AddUser);

            DeleteProducerCommand = new RelayCommand<object>(DeleteProducer);
            ModifyProducerCommand = new RelayCommand<object>(ModifyProducer);
            AddNewProducerCommand = new RelayCommand<object>(AddProducer);

            DeleteCategoryCommand = new RelayCommand<object>(DeleteCategory);
            ModifyCategoryCommand = new RelayCommand<object>(ModifyCategory);
            AddNewCategoryCommand = new RelayCommand<object>(AddCategory);

            AddNewStockCommand = new RelayCommand<object>(AddStock);

            AddNewProductCommand = new RelayCommand<object>(AddProduct);
            ModifyProductCommand = new RelayCommand<object>(ModifyProduct);
            DeleteProductCommand = new RelayCommand<object>(DeleteProduct);

            ViewReceiptInfoCommand = new RelayCommand<object>(ViewReceiptInfo);

            DeleteOfferCommand = new RelayCommand<object>(DeleteOffer);
            AddOfferCommand = new RelayCommand<object>(AddOffer);
            ModifyOfferCommand = new RelayCommand<object>(ModifyOffer);

            GoToProducersCategoryListCommand = new RelayCommand<object>(GoToProducersCategoryList);
            GoToCategoryValueCommand = new RelayCommand<object>(GoToCategoryValue);
            GoToUserRevenueMonthlyCommand = new RelayCommand<object>(GoToUserRevenueMonthly);
            GoToHighestValueReceiptCommand = new RelayCommand<object>(GoToHighestValueReceipt);
            
        }

        private void DeleteUser(object? obj)
        {
            if (SelectedItem is not User usr) return;
            // Display confirmation dialog
            var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            // Proceed with deletion if the user confirms
            if (result == MessageBoxResult.Yes && SelectedItem is User user)
            {
                _userBLL.DeleteUser(user);
                Users.Remove(user);
            }
        }

        private void ModifyUser(object? obj)
        {
            Console.WriteLine("ModifyUser");
            if (obj is not object[] values) return;
            Console.WriteLine("ModifyUser2");
            Console.WriteLine(values[0]);
            if (values[0] is not User user || values[1] is not MainAdminPage currentPage)
            {
                return;
            }
            Console.WriteLine("ModifyUser3");

            var userEditPage = new EditUserPage(user);

            currentPage.NavigationService?.Navigate(userEditPage);
        }

        private void AddUser(object? obj)
        {
            if(obj is not MainAdminPage currentPage)
            {
                return;
            }
            var EditUserPage = new EditUserPage();
            currentPage.NavigationService?.Navigate(EditUserPage);
        }

        private void AddStock(object? obj)
        {
            if (obj is not MainAdminPage currentPage)
            {
                return;
            }
            var editStockPage = new Views.EditPages.EditOfferPage();
            currentPage.NavigationService?.Navigate(editStockPage);
        }

        private void DeleteProducer(object? obj)
        {
            if (SelectedItem is not Producer produ) return;
            if (new ProductBLL().GetAllProducts().Any(p => p.ProducerId == (SelectedItem as Producer).ProducerId))
            {
                MessageBox.Show("Cannot delete producer because it is used in a product.");
                return;
            }
            // Display confirmation dialog
            var result = MessageBox.Show("Are you sure you want to delete this producer?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            // Proceed with deletion if the user confirms
            if (result == MessageBoxResult.Yes && SelectedItem is Producer producer)
            {
                _producerBLL.DeleteProducer(producer);
                Producers.Remove(producer);
            }
        }

        private void AddProducer(object? obj)
        {
            if (obj is not MainAdminPage currentPage)
            {
                return;
            }
            var editProducerPage = new Views.EditPages.EditProducersPage();
            currentPage.NavigationService?.Navigate(editProducerPage);
        }

        private void ModifyProducer(object? obj)
        {
            if (obj is not object[] values) return;
            if (values[0] is not Producer producer || values[1] is not MainAdminPage currentPage)
            {
                return;
            }

            var producerEditPage = new EditProducersPage(producer);

            currentPage.NavigationService?.Navigate(producerEditPage);
        }

        private void AddCategory(object? obj)
        {
            if (obj is not MainAdminPage currentPage)
            {
                return;
            }
            var editCategoryPage = new Views.EditPages.EditCategoryPage();
            currentPage.NavigationService?.Navigate(editCategoryPage);
        }

        private void ModifyCategory(object? obj)
        {
            if (obj is not object[] values) return;
            if (values[0] is not Category category || values[1] is not MainAdminPage currentPage)
            {
                return;
            }

            var categoryEditPage = new EditCategoryPage(category);

            currentPage.NavigationService?.Navigate(categoryEditPage);
        }

        private void DeleteCategory(object? obj)
        {
            if(SelectedItem is not Category cat) return;
            if (new ProductBLL().GetAllProducts().Any(p => p.CategoryId == (SelectedItem as Category).CategoryId))
            {
                MessageBox.Show("Cannot delete category because it is used in a product.");
                return;
            }
            // Display confirmation dialog
            var result = MessageBox.Show("Are you sure you want to delete this category?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            // Proceed with deletion if the user confirms
            if (result == MessageBoxResult.Yes && SelectedItem is Category category)
            {
                Console.WriteLine("DeleteCategory");
                _categoryBLL.DeleteCategory(category);
                Categories.Remove(category);
            }
        }

        private void AddProduct(object? obj)
        {
            if (obj is not MainAdminPage currentPage)
            {
                return;
            }
            var editProductPage = new Views.EditPages.EditProductPage();
            currentPage.NavigationService?.Navigate(editProductPage);
        }

        private void ModifyProduct(object? obj)
        {
            if (obj is not object[] values) return;
            if (values[0] is not Product product || values[1] is not MainAdminPage currentPage)
            {
                return;
            }

            var productEditPage = new EditProductPage(product);

            currentPage.NavigationService?.Navigate(productEditPage);
        }

        private void DeleteProduct(object? obj)
        {
            if (SelectedItem is not Product prod) return;
            if (new StockBLL().GetAllStocks().Any(p => p.ProductId == (SelectedItem as Product).ProductId))
            {
                MessageBox.Show("Cannot delete product because it is used in a stock.");
                return;
            }
            // Display confirmation dialog
            var result = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            // Proceed with deletion if the user confirms
            if (result == MessageBoxResult.Yes && SelectedItem is Product product)
            {
                _productBLL.DeleteProduct(product);
                Products.Remove(product);
            }
        }

        private void ViewReceiptInfo(object? obj)
        {
            if (obj is not object[] values) return;
            if (values[0] is not Receipt receipt || values[1] is not MainAdminPage currentPage)
            {
                return;
            }

            var viewReceiptInfo = new ViewReceiptInfo(receipt);

            currentPage.NavigationService?.Navigate(viewReceiptInfo);
        }

        private void DeleteOffer(object? obj)
        {
            if (SelectedItem is not Offer off) return;
            // Display confirmation dialog
            var result = MessageBox.Show("Are you sure you want to delete this offer?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            // Proceed with deletion if the user confirms
            if (result == MessageBoxResult.Yes && SelectedItem is Offer offer)
            {
                _offerBLL.DeleteOffer(offer);
                Offers.Remove(offer);
            }
        }

        private void AddOffer(object? obj)
        {
            if (obj is not MainAdminPage currentPage)
            {
                return;
            }
            var editOfferPage = new EditOffersPage();
            currentPage.NavigationService?.Navigate(editOfferPage);
        }

        private void ModifyOffer(object? obj)
        {
            if (obj is not object[] values) return;
            if (values[0] is not Offer offer || values[1] is not MainAdminPage currentPage)
            {
                return;
            }

            var offerEditPage = new EditOffersPage(offer);

            currentPage.NavigationService?.Navigate(offerEditPage);
        }

        private void GoToProducersCategoryList(object? obj)
        {
            if (obj is not MainAdminPage currentPage)
            {
                return;
            }
            var producerCategoryPage = new ProducerCategoriesPage();
            currentPage.NavigationService?.Navigate(producerCategoryPage);
        }

        private void GoToCategoryValue(object? obj)
        {
            if (obj is not MainAdminPage currentPage)
            {
                return;
            }
            var categoryValuePage = new CategoryValuePage();
            currentPage.NavigationService?.Navigate(categoryValuePage);
        }

        private void GoToHighestValueReceipt(object? obj)
        {
            if (obj is not MainAdminPage currentPage)
            {
                return;
            }
            var highestValueReceiptPage = new HighestValueReceiptPage();
            currentPage.NavigationService?.Navigate(highestValueReceiptPage);
        }

        private void GoToUserRevenueMonthly(object? obj)
        {
            if (obj is not MainAdminPage currentPage)
            {
                return;
            }
            var userRevenueMonthlyPage = new CashierRevenueMonthPage();
            currentPage.NavigationService?.Navigate(userRevenueMonthlyPage);
        }

      







    }
}
