using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ProdutsFilterSpecification :Specification<Product>
    {
        public ProdutsFilterSpecification(int? categoryId, int? brandId)
        {
            if (categoryId.HasValue)
            {
                Query.Where(c => c.CategoryId == categoryId);
            }
            if (brandId.HasValue)
            {
                Query.Where(b => b.BrandId == brandId);
            }
        }
    }
}
