using System;

namespace NDViet.UT.WS.AppConsole.Orders
{
    public interface IOrderService
    {
        Guid? Create(Order order);
    }
}