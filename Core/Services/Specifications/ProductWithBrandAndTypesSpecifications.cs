using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypesSpecifications : BaseSpecifications<Product ,int>
    {
        public ProductWithBrandAndTypesSpecifications(int id) : base(x => x.Id == id)
        {
            ApplyInclude();

        }
        public ProductWithBrandAndTypesSpecifications(ProductSpecificationsParamters SpecParamters) 
            :base(
            x =>
            (string.IsNullOrEmpty(SpecParamters.Search) || x.Name.ToLower().Contains(SpecParamters.Search.ToLower())) &&
            (!SpecParamters.BrandId.HasValue || x.BrandId == SpecParamters.BrandId) &&
            (!SpecParamters.TypeId.HasValue || x.TypeId == SpecParamters.TypeId)
            )
        {
            ApplyInclude();
            ApplySorting(SpecParamters.Sort);
            ApplyPagination(SpecParamters.PageIndex, SpecParamters.Pagesize);

        }
        private void ApplyInclude()
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
       private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            //if (sort != null)
            {
                switch (sort.ToLower())
                {

                    case "namedesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    case "priceasc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }

            }
            else
            {
                AddOrderBy(x => x.Name);
            }
        }

    }
    
}
