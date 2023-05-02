using Albelli.OrderManagement.Api.Controllers;
using Albelli.OrderManagement.Business.Contracts;
using Albelli.OrderManagement.DAL.Contracts.Repositories;
using Albelli.OrderManagement.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Albelli.OrderManagement.Api.Tests
{
    public class OrderControllerTests
    {

        private readonly Mock<IOrderRepository> _ordersRepoMock = new Mock<IOrderRepository>();
        private readonly Mock<IProductInfoRepository> _productInfoRepoMock = new Mock<IProductInfoRepository>();
        private readonly Mock<IPackageWidthCalculator> _pkgWidthCalcMock = new Mock<IPackageWidthCalculator>();

        [Fact]
        public void HealthCheck_Success()
        {
            var orderController = new OrdersController(_ordersRepoMock.Object, _productInfoRepoMock.Object, _pkgWidthCalcMock.Object);
            var rslt = orderController.HealthCheck().ConfigureAwait(false).GetAwaiter().GetResult();
            var resp = rslt.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public void HealthCheck_Fail()
        {
            Action a = () => { var ctrlr = new OrdersController(null, null, null); };
            a.Should().Throw<ArgumentNullException>();
            Action a1 = () => { var ctrlr = new OrdersController(_ordersRepoMock.Object, null, null); };
            a1.Should().Throw<ArgumentNullException>();
            Action a2 = () => { var ctrlr = new OrdersController(_ordersRepoMock.Object, _productInfoRepoMock.Object, null); };
            a2.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Get_Failure()
        {
            var orderController = new OrdersController(_ordersRepoMock.Object, _productInfoRepoMock.Object, _pkgWidthCalcMock.Object);
            orderController.RetrieveOrder(int.MinValue).ConfigureAwait(false).GetAwaiter().GetResult().Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Get_Success()
        {
            var orderController = new OrdersController(_ordersRepoMock.Object, _productInfoRepoMock.Object, _pkgWidthCalcMock.Object);
            _ordersRepoMock.Setup(r => r.Get(1))
                .Returns(Task.FromResult(new Order() { OrderId = 1 }))
                .Verifiable();
            var rslt = orderController.RetrieveOrder(1).ConfigureAwait(false).GetAwaiter().GetResult().Should().BeOfType<OkObjectResult>().Subject;
            rslt.Value.Should().NotBeNull();
            rslt.Value.Should().BeOfType<Order>();
        }

        [Fact]
        public void PlaceOrder_Failure()
        {
            var orderController = new OrdersController(_ordersRepoMock.Object, _productInfoRepoMock.Object, _pkgWidthCalcMock.Object);
            orderController.PlaceOrder(null).ConfigureAwait(false).GetAwaiter().GetResult().Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void PlaceOrder_Success()
        {
            var orderController = new OrdersController(_ordersRepoMock.Object, _productInfoRepoMock.Object, _pkgWidthCalcMock.Object);
            
            var lines = new List<OrderLine>();
            lines.Add(new OrderLine { ProductType = ProductType.Mug, Quantity = 5 });

            var res = orderController.PlaceOrder(lines).ConfigureAwait(false).GetAwaiter().GetResult().Should().BeOfType<OkObjectResult>().Subject;
            res.Value.Should().NotBeNull();
        }

    }
}
