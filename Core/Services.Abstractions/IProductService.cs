using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IProductService
    {
        // Define methods for product management
        // Get all products
        //Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(int? brandId, int? typeId , string? sort ,int pageIndex=1, int pageSize=5);
        Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParamters SpecParamters);
        // Get product by ID
        Task<ProductResultDto?> GetProductByIdAsync(int id);
        // Get All Brands
        Task<IEnumerable<BrandResultDto>> GetAllBrandAsync();
        // Get products by types
        Task<IEnumerable<TypeResultDto>> GetAllTypeAsync();
        // Add a new product
        //Task AddProductAsync(ProductResultDto product);
        //// Update an existing product
        //Task UpdateProductAsync(ProductResultDto product);
        //// Delete a product
        //Task DeleteProductAsync(int id);
        
       
    }
}
