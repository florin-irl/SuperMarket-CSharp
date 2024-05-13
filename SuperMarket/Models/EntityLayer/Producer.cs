﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Models.EntityLayer
{
    public class Producer
    {
        public int? ProducerId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool? IsActive { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }

    }
}
