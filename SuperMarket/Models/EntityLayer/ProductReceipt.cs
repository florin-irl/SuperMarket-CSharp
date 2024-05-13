using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Models.EntityLayer
{
    public class ProductReceipt
    {
        public int ProductId { get; set; }
        public int ReceiptId { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public bool? IsActive { get; set; }

        public virtual Receipt Receipt { get; set; }
        public virtual Product Product { get; set; }

    }
}
