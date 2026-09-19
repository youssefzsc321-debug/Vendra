using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Common;
using Vendra.Core.Interfaces;

namespace Vendra.Infrastructure.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEnitiy
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> Criteria)
        {
            this.Criteria = Criteria; 
        }
        public void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include); 
        }
    }
}
