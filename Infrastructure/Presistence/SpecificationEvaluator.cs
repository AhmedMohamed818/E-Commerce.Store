using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity , TKey>
            (IQueryable<TEntity> inputQuery,
            ISpecifications<TEntity, TKey> spec)
            where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if(spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.IncludeExpressions.Aggregate(query, (currentquery, includeExpression) => currentquery.Include(includeExpression));
            // spec.IncludeExpressions?.ForEach(include => query = query.Include(include));
            //if (specification.OrderBy != null)
            //{
            //    query = query.OrderBy(specification.OrderBy);
            //}
            //else if (specification.OrderByDescending != null)
            //{
            //    query = query.OrderByDescending(specification.OrderByDescending);
            //}
            //if (specification.IsPagingEnabled)
            //{
            //    query = query.Skip(specification.Skip).Take(specification.Take);
            //}
            return query;
        }
    }
}
