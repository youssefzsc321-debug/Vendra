using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Common;
namespace Vendra.Core.Interfaces
{
    public interface IGenericRepo<T> where T : BaseEnitiy
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        
        
        Task<T?> GetByIdAsync(object id);

        Task<T?> GetByIdWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllAsyncWithSpec(ISpecification<T> spec);
        
        
    }
}
