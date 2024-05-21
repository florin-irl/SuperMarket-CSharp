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
using System.Windows.Input;

namespace SuperMarket.ViewModels
{
    public class EditStockPageViewModel : BaseViewModel
    {
        public int Quantity { get; set; }
        public string Unit { get; set; }

        private DateTime _supplyDate;
        public DateTime SupplyDate
        {
            get { return _supplyDate; }
            set
            {
                _supplyDate = value;
                OnPropertyChanged("SupplyDate");
            }
        }

        private DateTime _expirationDate;
        public DateTime ExpirationDate
        {
            get { return _expirationDate; }
            set
            {
                _expirationDate = value;
                OnPropertyChanged("ExpirationDate");
            }
        }
        public int ProductId { get; set; }
        public decimal AquisitionCost { get; set; }

        private ObservableCollection<string> _products;        
        public ObservableCollection<string> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged("Products");
            }
        }
    

        public string ProductName { get; set; }

        private StockBLL StockBLL { get; set; } = new StockBLL();
        private ProductBLL ProductBLL { get; set; } = new ProductBLL();
        public ICommand AddNewStockCommand { get; set; }

        public EditStockPageViewModel()
        {
            AddNewStockCommand = new RelayCommand<object>(AddNewStock);
            ObservableCollection<Product> productss = ProductBLL.GetAllProducts();
            Products = new ObservableCollection<string>();
            foreach (var product in productss)
            {
                Products.Add(product.Name);
            }
            SupplyDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            ExpirationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        public void AddNewStock(object? obj)
        {
            if (obj is not EditStockPage editUserPage)
            {
                return;
            }
            if(string.IsNullOrEmpty(ProductName) || Quantity == 0 || string.IsNullOrEmpty(Unit) || SupplyDate == null || ExpirationDate == null || AquisitionCost == 0)
            {
                return;
            }
            ObservableCollection<Product> products = ProductBLL.GetAllProducts();
            Stock stock = new Stock
            {
                Quantity = Quantity,
                Unit = Unit,
                SupplyDate = SupplyDate,
                ExpirationDate = ExpirationDate,
                ProductId = (from p in products where p.Name == ProductName select p.ProductId).FirstOrDefault() ?? 0,
                AquisitionCost = AquisitionCost
            };
            StockBLL.AddStock(stock);
            var mainAdminPage = new MainAdminPage();
            editUserPage.NavigationService.Navigate(mainAdminPage);
        }

    }
}
