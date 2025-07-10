using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithCountSpecifications : BaseSpecifications<Product , int>
    {
        public ProductWithCountSpecifications(ProductSpecificationsParamters SpecParamters ) 
            : 
            base(
                x =>
                (string.IsNullOrEmpty(SpecParamters.Search) || x.Name.ToLower().Contains(SpecParamters.Search.ToLower())) &&
             (!SpecParamters.BrandId.HasValue || x.BrandId == SpecParamters.BrandId) &&
            (!SpecParamters.TypeId.HasValue || x.TypeId == SpecParamters.TypeId)
            )
        {

        }
    }
    
}
