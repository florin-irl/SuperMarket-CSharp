using SuperMarket.Models.DataAccessLayer;
using SuperMarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Models.BusinessLogicLayer
{
    public class ProducerBLL
    {
        private ProducerDAL _producerDAL = new ProducerDAL();

        public ObservableCollection<Producer> GetAllProducers()
        {
            return _producerDAL.GetAllProducers();
        }
    }
}
