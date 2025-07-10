using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ProductsController (IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet] // GET : /api/products
        [Cache (100)]
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductSpecificationsParamters SpecParamters)
        {
            // Simulate fetching products from a database
           var result = await serviceManager.ProductService.GetAllProductsAsync(SpecParamters);
            if (result is null ) return BadRequest(); //400
            return Ok(result); //200


        }
        [HttpGet("{id}")] // GET : /api/products/{id}
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await serviceManager.ProductService.GetProductByIdAsync(id);
            if (result is null) return NotFound(); //404
            return Ok(result); //200
        }
       
        
        [HttpGet("brands")] // GET : /api/products/Brands
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await serviceManager.ProductService.GetAllBrandAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result); //200
        }
        [HttpGet("types")] // GET : /api/Types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await serviceManager.ProductService.GetAllTypeAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result); //200
        }
    }
}
