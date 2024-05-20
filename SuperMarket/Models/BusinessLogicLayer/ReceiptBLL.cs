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
    public class ReceiptBLL
    {
        private ReceiptDAL _receiptDAL = new ReceiptDAL();

        public ObservableCollection<Receipt> GetAllReceipts()
        {
            return _receiptDAL.GetAllReceipts();
        }

        public void AddReceipt(Receipt receipt)
        {
            _receiptDAL.AddReceipt(receipt);
        }
    }
}
