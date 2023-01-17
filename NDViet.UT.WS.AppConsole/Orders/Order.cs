using System.Collections.Generic;
using System;

namespace NDViet.UT.WS.AppConsole.Orders
{
    public class Order
    {
        public Guid Id { set; get; }
        public Guid CardId { set; get; }
        public string TransactionId { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime ModifiedDate { set; get; }
        public class Detail
        {
            public Guid ProductID { get; set; }
            public string Product { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
        public List<Detail> Details { get; set; }
    }
}