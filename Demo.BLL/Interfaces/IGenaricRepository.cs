using Demo.BLL.Specifications;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> GetCountAsync(ISpecifications<T> specifications);
        Task<T> GetEntityWithSpec(ISpecifications<T> specifications);
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecifications<T> specifications);

    }
}
