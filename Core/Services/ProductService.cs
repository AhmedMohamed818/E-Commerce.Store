using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper) : IProductService
    {

        public async Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParamters SpecParamters)
        {
            var spec = new ProductWithBrandAndTypesSpecifications(SpecParamters);
            // Get all products from the repository
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            var countSpec = new ProductWithCountSpecifications(SpecParamters);


            var totalCount = await unitOfWork.GetRepository<Product, int>().CountAsync(countSpec);
            // Map the products to ProductResultDto
            var productDtos = mapper.Map<IEnumerable<ProductResultDto>>(products);
            // Return the mapped product DTOs
            return new PaginationResponse<ProductResultDto>(SpecParamters.PageIndex , SpecParamters.Pagesize , totalCount, productDtos);

        }
        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndTypesSpecifications(id);
            // Get product by ID from the repository
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            // Check if the product exists
            if (product is null) throw new ProductNotFoundException(id);

            // Map the product to ProductResultDto
            var productDto = mapper.Map<ProductResultDto>(product);
            // Return the mapped product DTO
            return productDto;
        }
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandAsync()
        {
            // Get all brands from the repository
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            // Map the brands to BrandResultDto
            var brandDtos = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            // Return the mapped brand DTOs
            return brandDtos;

        }
        public async Task<IEnumerable<TypeResultDto>> GetAllTypeAsync()
        {
            // Get all types from the repository
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            // Map the types to TypeResultDto
            var typeDtos = mapper.Map<IEnumerable<TypeResultDto>>(types);
            // Return the mapped type DTOs
            return typeDtos;
        }

        
    }
}
