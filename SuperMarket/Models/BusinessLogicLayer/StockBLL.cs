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
    public class StockBLL
    {
        private StockDAL _stockDAL = new StockDAL();

        public ObservableCollection<Stock> GetAllStocks()
        {
            return _stockDAL.GetAllStocks();
        }

        public void AddStock(Stock stock)
        {
            _stockDAL.AddStock(stock);
        }
    }
}
