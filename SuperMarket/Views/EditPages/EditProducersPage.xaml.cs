﻿using SuperMarket.Models.EntityLayer;
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
    /// Interaction logic for EditProducersPage.xaml
    /// </summary>
    public partial class EditProducersPage : Page
    {
        public EditProducersPage()
        {
            InitializeComponent();
            DataContext = new ViewModels.EditProducersPageViewModel();
        }

        public EditProducersPage(Producer producer)
        {
            InitializeComponent();
            DataContext = new ViewModels.EditProducersPageViewModel(producer);
        }
    }
}
