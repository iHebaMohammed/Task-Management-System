using Demo.BLL.Interfaces;
using Demo.BLL.Specifications;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TaskManagmentSystemContext _context;

        public GenericRepository(TaskManagmentSystemContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();
        public async Task<T> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);
        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecifications<T> specifications)
            => await ApplySpecifications(specifications).ToListAsync();
        public async Task<T> GetEntityWithSpec(ISpecifications<T> specifications)
         => await ApplySpecifications(specifications).FirstOrDefaultAsync();
        private IQueryable<T> ApplySpecifications(ISpecifications<T> specifications)
            => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specifications);

        public async Task<int> GetCountAsync(ISpecifications<T> specifications)
        => await ApplySpecifications(specifications).CountAsync();

        public async Task Create(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            await _context.Set<T>().AddAsync(entity);
        }
        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        => _context.Set<T>().Remove(entity);
    }
}
