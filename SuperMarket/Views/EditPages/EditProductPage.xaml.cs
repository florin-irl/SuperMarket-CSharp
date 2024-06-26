﻿using SuperMarket.Models.EntityLayer;
using SuperMarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMarket.Views.EditPages
{
    /// <summary>
    /// Interaction logic for EditProductPage.xaml
    /// </summary>
    public partial class EditProductPage : Page
    {
        public EditProductPage()
        {
            InitializeComponent();
            DataContext = new EditProductPageViewModel();
        }

        public EditProductPage(Product product)
        {
            InitializeComponent();
            DataContext = new EditProductPageViewModel(product);
        }
    }
}
