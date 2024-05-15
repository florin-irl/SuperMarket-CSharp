﻿using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Models.EntityLayer;
using SuperMarket.ViewModels.Commands;
using SuperMarket.Views.EditPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
        public object? SelectedItem { get; set; }
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
        }

        private void DeleteUser(object? obj)
        {
            // Display confirmation dialog
            var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            // Proceed with deletion if the user confirms
            if (result == MessageBoxResult.Yes && SelectedItem is User user)
            {
                _userBLL.DeleteUser(user);
                Users.Remove(user);
            }
        }







    }
}
