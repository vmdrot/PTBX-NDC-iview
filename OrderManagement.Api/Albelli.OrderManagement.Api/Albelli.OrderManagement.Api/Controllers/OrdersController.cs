using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Albelli.OrderManagement.Business.Contracts;
using Albelli.OrderManagement.DAL.Contracts.Repositories;
using Albelli.OrderManagement.Models;
using Albelli.OrderManagement.WebAPICommon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Albelli.OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductInfoRepository _productInfoRepository;
        private readonly IPackageWidthCalculator _packageWidthCalculator;

        public OrdersController(IOrderRepository orderRepository, IProductInfoRepository productInfoRepository,
                                IPackageWidthCalculator packageWidthCalculator)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _productInfoRepository = productInfoRepository ?? throw new ArgumentNullException(nameof(productInfoRepository));
            _packageWidthCalculator = packageWidthCalculator ?? throw new ArgumentNullException(nameof(packageWidthCalculator));
        }

        [HttpPost("place")]
        [ErrorFilter]
        public async Task<IActionResult> PlaceOrder([FromBody] IEnumerable<OrderLine> items)
        {
            if (!ModelState.IsValid || items == null || !items.Any())
                return BadRequest(ModelState);
            var order = new Order { Items = items, MinPackageWidth = await _packageWidthCalculator.Calculate(items, _productInfoRepository) };

            await _orderRepository.Add(order);

            return Ok(order);
        }

        [HttpGet("{orderId}")]
        [ErrorFilter]
        public async Task<IActionResult> RetrieveOrder(int orderId)
        {
            var order = await _orderRepository.Get(orderId);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpGet("ping")]
        [AllowAnonymous()]
#pragma warning disable 1998
        public async Task<IActionResult> HealthCheck()
#pragma warning restore 1998
        {
            return Ok("PONG");
        }
    }
}
