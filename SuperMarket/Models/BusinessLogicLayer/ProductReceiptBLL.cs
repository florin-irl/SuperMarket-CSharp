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
    public class ProductReceiptBLL
    {
        private ProductReceiptDAL _productReceiptDAL = new ProductReceiptDAL();

        public ObservableCollection<ProductReceipt> GetAllProductReceipts()
        {
            return _productReceiptDAL.GetAllProductReceipts();
        }

    }
}
