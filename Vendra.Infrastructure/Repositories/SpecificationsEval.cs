using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Common;
using Vendra.Core.Interfaces;
using Vendra.Infrastructure.Specifications;

namespace Vendra.Infrastructure.Repositories
{
    public class SpecificationsEval<TEnity> where TEnity : BaseEnitiy
    {
        public static IQueryable<TEnity> GetQuery(IQueryable<TEnity> inputQuery, ISpecification<TEnity>spec)
        {
            var query = inputQuery.AsQueryable();
            if(spec.Criteria != null)
            {
                query=query.Where(spec.Criteria);
            }

            if(spec.Includes!=null&&spec.Includes.Any())
            {
                foreach(var include in spec.Includes)
                {
                    query=query.Include(include);
                }
            }
            return query;
        }
    }
}
