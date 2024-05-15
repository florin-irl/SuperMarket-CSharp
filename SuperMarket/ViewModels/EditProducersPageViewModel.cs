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
using System.Windows.Input;

namespace SuperMarket.ViewModels
{
    public class EditProducersPageViewModel : BaseViewModel
    {
        public ICommand SaveCommand { get; set; }

        public string Name { get; set; }
        public string Country { get; set; }

        public bool Add;

        private ProducerBLL _producerBLL = new ProducerBLL();

        public EditProducersPageViewModel()
        {
            SaveCommand = new RelayCommand<object>(SaveProducer);
            Add = true;
        }

        public void SaveProducer(object? obj)
        {
            if (obj is not EditProducersPage editProducersPage)
            {
                return;
            }
            if (Add == false)
            {
                return;
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
