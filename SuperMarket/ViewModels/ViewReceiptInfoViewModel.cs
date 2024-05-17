using SuperMarket.Models.DataAccessLayer;
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
    public class ViewReceiptInfoViewModel : BaseViewModel
    {
        private Receipt _receipt;
        public Receipt Receipt
        {
            get { return _receipt; }
            set
            {
                _receipt = value;
                OnPropertyChanged("Receipt");
            }
        }

        public ICommand OkCommand { get; set; }

        private ProductReceiptDAL _productReceiptDAL = new ProductReceiptDAL();
        private ProductDAL _productDAL = new ProductDAL();


        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<ProductReceipt> ProductReceipts { get; set; }

        public ViewReceiptInfoViewModel()
        {
            Receipt = new Receipt();
            Products = new ObservableCollection<Product>();
            ProductReceipts = new ObservableCollection<ProductReceipt>();
            OkCommand = new RelayCommand<object>(Ok);
        }

        public ViewReceiptInfoViewModel(Receipt receipt)
        {
            Receipt = receipt;
            Products = new ObservableCollection<Product>();
            ProductReceipts = new ObservableCollection<ProductReceipt>();
            ProductReceipts = _productReceiptDAL.GetProductReceiptsByReceiptId(Receipt.ReceiptID);
            foreach (ProductReceipt productReceipt in ProductReceipts)
            {
                Products.Add(_productDAL.GetProductById(productReceipt.ProductId));
            }
            OkCommand = new RelayCommand<object>(Ok);
        }

        public void Ok(object? obj)
        {
            if(obj is not ViewReceiptInfo viewReceiptInfo)
            {
                return;
            }
            var mainAdminPage = new MainAdminPage();
            viewReceiptInfo.NavigationService.Navigate(mainAdminPage);
        }

    }
}
