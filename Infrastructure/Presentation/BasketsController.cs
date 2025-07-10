using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController( IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet] // GET api/baskets/{id}
        public async Task<IActionResult> GetBasketById(string id)
        {
            var basket = await serviceManager.BasketService.GetBasketAsync(id);
            return Ok(basket);

        }
        [HttpPost] // POST api/baskets
        public async Task<IActionResult> UpdateBasket( BasketDto basketDto)
        {
            var updatedBasket = await serviceManager.BasketService.UpdateBasketAsync(basketDto);
            return Ok(updatedBasket);
        }
        [HttpDelete("{id}")] // DELETE api/baskets/{id}
        public async Task<IActionResult> DeleteBasket(string id)
        {
            var result = await serviceManager.BasketService.DeleteBasketAsync(id);
            
             return NoContent(); // 204 No Content
            
        }





















    }
}
