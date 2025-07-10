using Domain.Models.OrderModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrders(OrderRequestDto Request)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            //  var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var result = await serviceManager.OrderService.CreateOrderAsync(Request, Email);
            if (result is null)
            {
                return BadRequest("Order creation failed.");
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrderService.GetOrdersByUserEmailAsync(Email);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {

            var result = await serviceManager.OrderService.GetOrdersByIdAsync(id);
            return Ok(result);
        }
        [HttpGet("DeliveryMethod")]
        public async Task<IActionResult> GetAllDeliveryMethod()
        {
            var result = await serviceManager.OrderService.GetAllDeliveryMethods();
            return Ok(result);

        }
    }
}