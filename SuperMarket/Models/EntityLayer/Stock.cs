using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Models.EntityLayer
{
    public class Stock
    {
        public int? StockId { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime SupplyDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ProductId { get; set; }
        public decimal AquisitionCost { get; set; }
        public decimal SellingPrice => AquisitionCost * decimal.Parse(ConfigurationManager.AppSettings["PriceVAT"]);
        public bool? IsActive { get; set; }

        public virtual Product Product { get; set; }

    }
}
