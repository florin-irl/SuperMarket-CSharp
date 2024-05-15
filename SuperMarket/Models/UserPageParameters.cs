using SuperMarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SuperMarket.Models
{
    public class UserPageParameters
    {
        public User User { get; set; }
        public Page CurrentPage { get; set; }
    }

}
