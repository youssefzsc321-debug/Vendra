using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Common;
using Vendra.Core.Data;
using Vendra.Core.Interfaces;
using Vendra.Infrastructure.Specifications;

namespace Vendra.Infrastructure.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEnitiy
    {
        private readonly VendraContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepo(VendraContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<int> CountAsyncSpec(ISpecification<T> spec)
        {
            return await ApplySepcification(spec).CountAsync();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsyncWithSpec(ISpecification<T> spec)
        {
            return await ApplySepcification(spec).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySepcification(spec).FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        private IQueryable<T> ApplySepcification(ISpecification<T>spec)
        {
            return SpecificationsEval<T>.GetQuery(_dbSet, spec);
        }
    }
}
