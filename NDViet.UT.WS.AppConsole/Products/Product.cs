﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NDViet.UT.WS.AppConsole.Products
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
    }
}
