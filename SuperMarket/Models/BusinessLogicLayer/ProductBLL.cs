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

        public void AddProduct(Product product)
        {
            _productDAL.AddProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            _productDAL.ModifyProduct(product);
        }

        public void DeleteProduct(Product product)
        {
            _productDAL.DeleteProduct(product);
        }
    }
}
