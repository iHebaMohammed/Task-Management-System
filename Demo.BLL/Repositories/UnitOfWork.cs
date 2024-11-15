using Demo.BLL.Interfaces;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskManagmentSystemContext _context;
        private Hashtable _repositories;

        public UnitOfWork(TaskManagmentSystemContext context)
        {
            _context = context;
        }

        public async Task<int> Complete() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories is null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}
