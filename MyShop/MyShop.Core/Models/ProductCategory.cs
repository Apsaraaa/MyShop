﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory :BaseEntity
    {
        
        public string category { get; set; }
        

        public ProductCategory Find(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
