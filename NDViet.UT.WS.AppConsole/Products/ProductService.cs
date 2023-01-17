using NDViet.UT.WS.AppConsole.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NDViet.UT.WS.AppConsole.Products
{
    public class ProductService : IProductService
    {
        public bool IsValid(Order.Detail item)
        {
            Console.WriteLine($"[Real] Check productId {item.ProductID} success!");
            return true;
        }
    }
}
