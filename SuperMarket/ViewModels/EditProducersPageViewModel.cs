using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Models.EntityLayer;
using SuperMarket.ViewModels.Commands;
using SuperMarket.Views;
using SuperMarket.Views.EditPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SuperMarket.ViewModels
{
    public class EditProducersPageViewModel : BaseViewModel
    {
        private string _name;
        private string _country;
        private int? _id;
        public ICommand SaveCommand { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public int? Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public bool Add;

        private ProducerBLL _producerBLL = new ProducerBLL();

        public EditProducersPageViewModel()
        {
            SaveCommand = new RelayCommand<object>(SaveProducer);
            Add = true;
        }

        public EditProducersPageViewModel(Producer producer)
        {
            SaveCommand = new RelayCommand<object>(SaveProducer);
            Add = false;
            Name = producer.Name;
            Country = producer.Country;
            Id = producer.ProducerId;
        }

        public void SaveProducer(object? obj)
        {
            if (obj is not EditProducersPage editProducersPage)
            {
                return;
            }
            var producers = _producerBLL.GetAllProducers();
            var isUnique = !producers.Any(producer => producer.Name == Name && producer.ProducerId != Id);

            if (isUnique == false)
            {
                var warning = MessageBox.Show("Producer already exists", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Add == false)
            {
                if (Name == "" || Country == "")
                {
                    return;
                }

                Producer producer = new Producer();
                producer.ProducerId = Id;
                producer.Name = Name;
                producer.Country = Country;
                _producerBLL.UpdateProducer(producer);

                var mainAdminPage = new MainAdminPage();
                editProducersPage.NavigationService.Navigate(mainAdminPage);
            }
            else
            {
                if (Name == "" || Country == "")
                {
                    return;
                }

                Producer producer = new Producer();
                producer.Name = Name;
                producer.Country = Country;
                _producerBLL.AddProducer(producer);

                var mainAdminPage = new MainAdminPage();
                editProducersPage.NavigationService.Navigate(mainAdminPage);
            }

        }

    }
}
