using Microsoft.Extensions.Logging;
using NDViet.UT.WS.AppConsole.Email;
using NDViet.UT.WS.AppConsole.Exceptions;
using NDViet.UT.WS.AppConsole.Payment;
using NDViet.UT.WS.AppConsole.Products;
using NDViet.UT.WS.AppConsole.Sessions;
using NDViet.UT.WS.AppConsole.Utilities;
using System;
using System.Collections.Generic;

namespace NDViet.UT.WS.AppConsole.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly IPaymentService _paymentService;
        private readonly ISessionService _sessionService;
        private readonly ILogger _logger;

        public OrderService(
            IOrderRepository orderRepository,
            IProductService productService,
            IPaymentService paymentService,
            ISessionService sessionService,
            ILogger<OrderService> logger)
        {
            this._orderRepository = orderRepository;
            this._productService = productService;
            this._paymentService = paymentService;
            this._sessionService = sessionService;
            _logger = logger;
        }

        /// <summary>
        /// Tạo hóa đơn
        /// </summary>
        /// <param name="order">Thông tin hóa đơn</param>
        /// <returns></returns>
        /// <exception cref="BussinessException"></exception>
        public Guid? Create(Order order)
        {

            var checkResult = IsValid(order);
            if (checkResult != null && checkResult.Count > 0)
            {
                throw new BussinessException("Err001", checkResult);
            }
            var logined = _sessionService.IsLoggedIn();
            if (!logined)
            {

                throw new BussinessException("LogErr01", "User need login!");
            }
            order.CreatedDate = DateTime.Now;
            var orderId = _orderRepository.Create(order);
            if (orderId == null || orderId == Guid.Empty)
            {
                _logger.LogError($"[{CommonFunction.DateTimeFormat(DateTime.Now)}] Create order failed");
                throw new BussinessException("Err002", "Create order failed!");
            }
            order.Id = (Guid)orderId;
            var cardInfo = _sessionService.GetCurrentUser().Cards.Find(c => c.CardId == order.CardId);
            var payment = new PaymentDto()
            {
                AccNumber = cardInfo.AccNumber,
                CardNumber = cardInfo.CardNumber,
                CurrencyCode = "VND",
                Money = CalculatorPrice(order),
                CreatedDate = DateTime.Now

            };
            var paymentResult = _paymentService.Create(payment);
            if (paymentResult == null || !paymentResult.Status)
            {
                _logger.LogError($"[{DateTime.Now}] create payment for order {order.Id} failed");
                UpdateStatusOrder(order.Id, 1);
                throw new BussinessException("Pay001", "Create payment failed!");
            }
            UpdateStatusOrder(order.Id, paymentResult);
            try
            {
                new MISAEmail().Send(new EmailDto()
                {
                    Content = EmailHelper.CreateOrderContent(_sessionService.GetCurrentUser(), order),
                    From = "no-reply@misa.com.vn",
                    To = _sessionService.GetCurrentUser().Email,
                    Subject = $"Đặt hàng thành công đơn hàng {order.Id}"

                });
            }
            catch (Exception ex)
            {

                _logger.LogError($"[{DateTime.Now}] Send email fail. Exception message {ex.Message}");
            }

            return orderId;
        }


        private void UpdateStatusOrder(Guid orderId, PaymentResult paymentResult)
        {
            _orderRepository.UpdateStatusWithPaymentTransactionId(orderId, 2, paymentResult.TransactionId);
        }

        private void UpdateStatusOrder(Guid orderId, int status)
        {
            _orderRepository.UpdateStatus(orderId, status);
        }

        private decimal CalculatorPrice(Order order)
        {
            decimal result = 0;
            foreach (var item in order.Details)
            {
                result += item.Price;
            }
            return result;
        }

        private List<string> IsValid(Order order)
        {
            var result = new List<string>();
            foreach (var item in order.Details)
            {
                if (!_productService.IsValid(item))
                {
                    result.Add($"ProductId {item.ProductID} invalid!");
                }
            }
            return result;
        }
    }
}
