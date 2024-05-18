using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Models.EntityLayer;
using SuperMarket.ViewModels.Commands;
using SuperMarket.Views;
using SuperMarket.Views.SpecialQueries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuperMarket.ViewModels
{
    public class HighestValueReceiptPageViewModel : BaseViewModel
    {
        public DateTime SelectedDate { get; set; }
        private ReceiptBLL _receiptBLL = new ReceiptBLL();
        private Receipt _foundReceipt = new Receipt();
        private string _cashierName;
        private decimal _totalValue;
        private bool _canListReceipts;
        public bool CanListReceipts
        {
            get { return _canListReceipts; }
            set
            {
                _canListReceipts = value;
                OnPropertyChanged("CanListReceipts");
            }
        }
        public ICommand FindHighestReceiptCommand { get; set; }
        public ICommand ViewReceiptInfoCommand { get; set; }

        public string CashierName
        {
            get { return _cashierName; }
            set
            {
                _cashierName = value;
                OnPropertyChanged("CashierName");
            }
        }

        public decimal TotalValue
        {
            get { return _totalValue; }
            set
            {
                _totalValue = value;
                OnPropertyChanged("TotalValue");
            }
        }
        public Receipt FoundReceipt
        {
            get { return _foundReceipt; }
            set
            {
                _foundReceipt = value;
                OnPropertyChanged("FoundReceipt");
            }
        }

        public HighestValueReceiptPageViewModel()
        {
            SelectedDate = DateTime.Now;
            FoundReceipt = null;
            FindHighestReceiptCommand = new RelayCommand<object>(FindHighestValueReceipt);
            ViewReceiptInfoCommand = new RelayCommand<object>(ViewReceiptInfo);
            CanListReceipts = false;
        }

        public void FindHighestValueReceipt(object? obj)
        {
            Console.WriteLine("AICI!!!!");

            if (obj is not HighestValueReceiptPage currentPage)
            {
                return;
            }
            ObservableCollection<Receipt> receipts = new ObservableCollection<Receipt>(_receiptBLL.GetAllReceipts());
            FoundReceipt = receipts.Where(receipt => receipt.IssueDate == SelectedDate).OrderByDescending(receipt => receipt.TotalPrice).FirstOrDefault();
            Console.WriteLine(FoundReceipt);
            if (FoundReceipt != null)
            {
                CashierName = FoundReceipt.User.Username;
                TotalValue = FoundReceipt.TotalPrice;
                CanListReceipts= true;
            }
            else
            {
                CashierName = "No receipt found";
                TotalValue = 0;
                CanListReceipts = false;
            }
        }

        private void ViewReceiptInfo(object? obj)
        {
            if (obj is not object[] values) return;
            if (values[0] is not Receipt receipt || values[1] is not HighestValueReceiptPage currentPage)
            {
                return;
            }

            var viewReceiptInfo = new ViewReceiptInfo(receipt);

            currentPage.NavigationService?.Navigate(viewReceiptInfo);
        }
    }
}
