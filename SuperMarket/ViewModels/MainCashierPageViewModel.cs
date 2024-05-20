using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Models.EntityLayer;
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

        public ObservableCollection<Product> CartList { get; set; }
        
        public string SearchText { get; set; }

        public string SelectedFilter { get; set; }

        public Product SelectedProduct { get; set; }

        private ProductBLL _productBLL = new ProductBLL();

        public ICommand SearchCommand { get; set; }
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
            CartList = new ObservableCollection<Product>();
            SearchCommand = new RelayCommand<string>(Search);
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
    }
}
