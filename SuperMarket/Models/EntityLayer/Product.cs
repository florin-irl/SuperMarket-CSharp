using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Models.EntityLayer
{
    public class Product
    {
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public int CategoryId { get; set; }
        public int ProducerId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Category Category { get; set; }

        public virtual Producer Producer { get; set; }

        public virtual Offer? Offer { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }


    }
}
