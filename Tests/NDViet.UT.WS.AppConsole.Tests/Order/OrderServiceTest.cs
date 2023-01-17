using Microsoft.Extensions.Logging;
using NDViet.UT.WS.AppConsole.Exceptions;
using NDViet.UT.WS.AppConsole.Orders;
using NDViet.UT.WS.AppConsole.Payment;
using NDViet.UT.WS.AppConsole.Products;
using NDViet.UT.WS.AppConsole.Sessions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VerifyNUnit;
using static NDViet.UT.WS.AppConsole.Orders.Order;

namespace NDViet.UT.WS.AppConsole.OrderServiceTest
{
    public class OrderServiceTest
    {
        private IOrderRepository _orderRepository;
        private IProductService _productService;
        private IPaymentService _paymentService;
        private ISessionService _sessionService;
        private ILogger _logger;
        private OrderService _orderService;
        private Order order = new Order()
        {
            Details = new List<Detail>() {
                    new Detail()
                    {
                        ProductID = Guid.NewGuid()
                    }
               }
        };

        [SetUp]
        public void Setup()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
            _productService = Substitute.For<IProductService>();
            _paymentService = Substitute.For<IPaymentService>();
            _sessionService = Substitute.For<ISessionService>();
            _logger = Substitute.For<ILogger<OrderService>>();

            _orderService = Substitute.For<OrderService>(
                _orderRepository,
                _productService,
                _paymentService,
                _sessionService,
                _logger
               );
        }

        [Test]
        public void Create_OrderIsValid_ThrowBusinessException()
        {
            //Arrange
            _productService.IsValid(Arg.Any<Detail>()).Returns(false);

            var orderDetailNotNull = order;

            //Act
            var result = Assert.Throws<BussinessException>(() => _orderService.Create(orderDetailNotNull));

            //Assert
            //Verifier.Verify()
            Assert.IsNotNull(result);
            Assert.That(result.Errors[0].ErrorCode == "Err001");
        }

        [Test]
        public void Create_SessionNotLogin_ThrowBusinessException()
        {
            // Arrange
            _productService.IsValid(Arg.Any<Detail>()).Returns(true);
            _sessionService.IsLoggedIn().Returns(false);

            var sessionLoggedIn = this.order;
            //Act
            var result = Assert.Throws<BussinessException>(() => _orderService.Create(sessionLoggedIn));

            //Assert
            Assert.That(result.ErrorCode == "LogErr01");
        }

        [Test]
        public Task Create_OrderNotHaveID_LogErrorAndThrowEX()
        {
            //Arrange
            _productService.IsValid(Arg.Any<Detail>()).Returns(true);
            _sessionService.IsLoggedIn().Returns(true);

            var orderHasID = this.order;
            _orderRepository.Create(orderHasID).Returns(Guid.Empty);
            //Act
            var result = Assert.Throws<BussinessException>(() => _orderService.Create(orderHasID));
            
            //Assert
            return Verifier.Verify(result).IgnoreStackTrace();
        }
    }
}