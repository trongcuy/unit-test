using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace NDViet.UT.WS.AppConsole.Payment
{
    public class PaymentService : IPaymentService
    {
        

        public PaymentResult Create(PaymentDto payment)
        {
            Console.WriteLine("[Real] Create payment success!");
            return new PaymentResult()
            {
                TransactionId = Guid.NewGuid().ToString(),
                Status = true
            };
        }
    }
}
