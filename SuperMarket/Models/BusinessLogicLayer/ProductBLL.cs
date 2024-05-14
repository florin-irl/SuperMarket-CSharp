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
    public class ProductBLL
    {
        private ProductDAL _productDAL = new ProductDAL();

        public ObservableCollection<Product> GetAllProducts()
        {
            return _productDAL.GetAllProducts();
        }
    }
}
