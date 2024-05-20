using SuperMarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Models.EntityLayer
{
    public class ProductPrice : BaseViewModel
    {
        public Product cartProduct { get; set; }
        public decimal cartPrice { get; set; }
    }
}
