using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Models.EntityLayer
{
    public class Receipt
    {
        public int? ReceiptID { get; set; }
        public DateTime IssueDate { get; set; }
        public int UserID { get; set; }
        public decimal TotalPrice { get; set; }
        public bool? IsActive { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
