using System;

namespace NDViet.UT.WS.AppConsole.Orders
{
    public interface IOrderRepository
    {
        Guid? Create(Order order);
        void UpdateStatus(Guid orderId, int status);
        void UpdateStatusWithPaymentTransactionId(Guid orderId, int status, string transactionId);
    }
}