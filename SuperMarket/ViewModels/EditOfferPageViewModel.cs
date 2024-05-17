using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Models.EntityLayer;
using SuperMarket.ViewModels.Commands;
using SuperMarket.Views.EditPages;
using SuperMarket.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuperMarket.ViewModels
{
    public class EditOfferPageViewModel : BaseViewModel
    {
        private int? _offerId;
        private int? _productId;
        private int _discountPercentage;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _reason;

        public int? OfferId
        {
            get { return _offerId; }
            set
            {
                _offerId = value;
                OnPropertyChanged("OfferId");
            }
        }

        public int? ProductId
        {
            get { return _productId; }
            set
            {
                _productId = value;
                OnPropertyChanged("ProductId");
            }
        }

        public int DiscountPercentage
        {
            get { return _discountPercentage; }
            set
            {
                _discountPercentage = value;
                OnPropertyChanged("DiscountPercentage");
            }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }
        public string Reason
        {
            get { return _reason; }
            set
            {
                _reason = value;
                OnPropertyChanged("Reason");
            }
        }

        public bool Add;

        public ICommand SaveCommand { get; set; }

        public string ProductName { get; set; }

        private ProductBLL _productBLL { get; set; } = new ProductBLL();
        private OfferBLL _offerBLL { get; set; } = new OfferBLL();

        public ObservableCollection<string> products { get; set; }

        public EditOfferPageViewModel()
        {
            SaveCommand = new RelayCommand<object>(Save);
            ObservableCollection<Product> productList = new ObservableCollection<Product>(_productBLL.GetAllProducts());
            Add=true;
            products = new ObservableCollection<string>();
            foreach (var product in productList)
            {
                products.Add(product.Name);
            }
        }

        public EditOfferPageViewModel(Offer offer)
        {
            SaveCommand = new RelayCommand<object>(Save);
            OfferId = offer.OfferId;
            ProductId = offer.ProductId;
            DiscountPercentage = offer.DiscountPercentage;
            StartDate = offer.StartDate;
            EndDate = offer.EndDate;
            Reason = offer.Reason;
            Add = false;
            ProductName = (from p in _productBLL.GetAllProducts() where p.ProductId == ProductId select p.Name).FirstOrDefault();
            ObservableCollection<Product> productList = new ObservableCollection<Product>(_productBLL.GetAllProducts());
            products = new ObservableCollection<string>();
            foreach (var product in productList)
            {
                products.Add(product.Name);
            }
        }

        public void Save(object? obj)
        {
            if (obj is not EditOffersPage editOfferPage)
            {
                return;
            }
            if (DiscountPercentage<0 || DiscountPercentage>100)
            {
                return;
            }

            if(StartDate>EndDate)
            {
                return;
            }

            if(ProductName==null)
            {
                return;
            }

           
            if (Add)
            {
                Offer offer = new Offer();
                offer.ProductId = (from p in _productBLL.GetAllProducts() where p.Name == ProductName select p.ProductId).FirstOrDefault();
                offer.DiscountPercentage = DiscountPercentage;
                offer.StartDate = StartDate;
                offer.EndDate = EndDate;
                offer.Reason = Reason;
                _offerBLL.AddOffer(offer);
            }
            else
            {
                Offer offer = new Offer();
                offer.OfferId = OfferId;
                offer.ProductId = (from p in _productBLL.GetAllProducts() where p.Name == ProductName select p.ProductId).FirstOrDefault();
                offer.DiscountPercentage = DiscountPercentage;
                offer.StartDate = StartDate;
                offer.EndDate = EndDate;
                offer.Reason = Reason;
                _offerBLL.UpdateOffer(offer);
            }

            var mainAdminPage = new MainAdminPage();
            editOfferPage.NavigationService.Navigate(mainAdminPage);
        }
    }

   
}
