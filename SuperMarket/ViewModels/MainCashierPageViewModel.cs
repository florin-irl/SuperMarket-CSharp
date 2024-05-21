using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Models.EntityLayer;
using SuperMarket.Services;
using SuperMarket.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuperMarket.ViewModels
{
    public class MainCashierPageViewModel : BaseViewModel
    {
        public ObservableCollection<String> FilterList { get; set; }

        private decimal _totalPrice;

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        private ObservableCollection<Product> _selectedProducts;
        public ObservableCollection<Product> SelectedProducts
        {
            get { return _selectedProducts; }
            set
            {
                _selectedProducts = value;
                OnPropertyChanged("SelectedProducts");
            }
        }

        private ObservableCollection<ProductPrice> _cartList;
        public ObservableCollection<ProductPrice> CartList
        {
            get { return _cartList; }
            set
            {
                _cartList = value;
                OnPropertyChanged("CartList");
            }
        }
        
        public string SearchText { get; set; }

        public string SelectedFilter { get; set; }

        public Product SelectedItem { get; set; }

        private ProductBLL _productBLL = new ProductBLL();
        private StockBLL _stockBLL = new StockBLL();
        private ReceiptBLL _receiptBLL = new ReceiptBLL();
        private ProductReceiptBLL _productReceiptBLL = new ProductReceiptBLL();

        public ICommand SearchCommand { get; set; }
        public ICommand AddToCartCommand { get; set; }

        public ICommand CreateReceiptCommand { get; set; }
        public MainCashierPageViewModel()
        {
            FilterList = new ObservableCollection<string>
            {
                "Name",
                "Barcode",
                "Category",
                "Producer",
                "Expiration Date"

            };

            SelectedProducts = new ObservableCollection<Product>();
            CartList = new ObservableCollection<ProductPrice>();
            SearchCommand = new RelayCommand<string>(Search);
            AddToCartCommand = new RelayCommand<Product>(AddToCart);
            CreateReceiptCommand = new RelayCommand<object>(CreateReceipt);
        }

        private void Search(string filter)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                SelectedProducts.Clear();
                return;
            }


            switch (SelectedFilter)
            {
                case "Name":
                    SelectedProducts = _productBLL.GetProductsByName(SearchText);
                    break;
                case "Barcode":
                    SelectedProducts = _productBLL.GetProductsByBarcode(SearchText);
                    break;
                case "Category":
                    SelectedProducts = _productBLL.GetProductsByCategory(SearchText);
                    break;
                case "Producer":
                    SelectedProducts = _productBLL.GetProductsByProducer(SearchText);
                    break;
                case "Expiration Date":
                    SelectedProducts = _productBLL.GetProductsByExpirationDate(SearchText);
                    break;
            }
            foreach(Product product in SelectedProducts)
            {
                Console.WriteLine(product.Name);
                Console.WriteLine("IM HERE");
            }
        }

        private void AddToCart(object? obj)
        {
            ProductPrice productPrice = new ProductPrice();
            productPrice.cartProduct = SelectedItem;
            Stock stock = new Stock();
            DateTime oldestDate = DateTime.Now;
            ObservableCollection<Stock> stocks = _stockBLL.GetAllStocks();
            foreach (Stock s in stocks)
            {
                if (s.ProductId == SelectedItem.ProductId && s.SupplyDate<oldestDate)
                {
                    oldestDate = s.SupplyDate;
                    stock = s;
                }
            }
            productPrice.cartPrice = stock.SellingPrice;
            stock.Quantity--;
            if(stock.Quantity == 0)
            {
                _stockBLL.DeleteStock(stock);
            }
            else
            {
                _stockBLL.UpdateStock(stock);
            }
            CartList.Add(productPrice);
            Console.WriteLine(productPrice.cartProduct.Name);
            Console.WriteLine(productPrice.cartPrice);
            TotalPrice += productPrice.cartPrice;
            SelectedProducts.Clear();
        }

        private void CreateReceipt(object? obj)
        {

            Receipt receipt = new Receipt
            {
                TotalPrice = TotalPrice,
                UserID = UserSession.Instance.LoggedInUser.UserId ?? 0,
                IssueDate = DateTime.Now
            };
            _receiptBLL.AddReceipt(receipt);
            var lastReceipt = _receiptBLL.GetAllReceipts()
                             .OrderByDescending(r => r.ReceiptID)
                             .FirstOrDefault();


            // Dictionary to hold product IDs and their quantities
            Dictionary<int, int> productQuantities = new Dictionary<int, int>();

            foreach (ProductPrice productPrice in CartList)
            {
                if (productQuantities.ContainsKey(productPrice.cartProduct.ProductId ?? 0))
                {
                    productQuantities[productPrice.cartProduct.ProductId??0] += 1 ;
                }
                else
                {
                    productQuantities[productPrice.cartProduct.ProductId ?? 0] = 1;
                }
            }

            foreach(KeyValuePair<int, int> productQuantity in productQuantities)
            {
                ProductReceipt productReceipt = new ProductReceipt
                {
                    ProductId = productQuantity.Key,
                    ReceiptId = lastReceipt.ReceiptID??0,
                    Quantity = productQuantity.Value,
                    Subtotal = CartList.Where(x => x.cartProduct.ProductId == productQuantity.Key).FirstOrDefault().cartPrice * productQuantity.Value
                };
                _productReceiptBLL.AddProductReceipt(productReceipt);
            }

            CartList.Clear();
            TotalPrice = 0;

        }
    }
}
