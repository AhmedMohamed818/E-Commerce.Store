﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; }
        //public string PictureUrl { get; set; }
        //public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
