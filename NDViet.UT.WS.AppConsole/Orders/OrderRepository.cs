using System;
using System.Collections.Generic;
using System.Text;

namespace NDViet.UT.WS.AppConsole.Orders
{
    public class OrderRepository : IOrderRepository
    {
        public Guid? Create(Order order)
        { 
            Console.WriteLine("[Real] OrderRepository create order success!");
            return Guid.NewGuid();
        }

        public void UpdateStatus(Guid orderId, int status)
        {
            Console.WriteLine($"[Real] Update status orderId {orderId}, status {status} success!");
        }

        public void UpdateStatusWithPaymentTransactionId(Guid orderId, int status, string transactionId)
        {
            Console.WriteLine($"[Real] Update status orderId: {orderId}, status: {status}, transactionId: {transactionId} success!");
        }
    }
}
