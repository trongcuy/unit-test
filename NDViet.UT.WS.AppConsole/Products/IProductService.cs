using NDViet.UT.WS.AppConsole.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NDViet.UT.WS.AppConsole.Products
{
    public interface IProductService
    {
        bool IsValid(Order.Detail item);
    }
}
