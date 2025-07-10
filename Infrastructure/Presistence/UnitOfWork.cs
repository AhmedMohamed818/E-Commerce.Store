using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Presistence.Data;
using Presistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
       // private readonly Dictionary<string, object> _repositories ;
        private readonly ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            //_repositories = new Dictionary<string, object>();
            _repositories = new ConcurrentDictionary<string, object>();

        }

        //public IGenericRepositories<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        //{
        //    // return new GenericRepositories<TEntity, TKey>(_context);

        //    //var type = typeof(TEntity).Name;
        //    //if (!_repositories.ContainsKey(type))
        //    //{
        //    //    var repositoryType = new GenericRepositories<TEntity, TKey>(_context);
        //    //    _repositories.Add(type, repositoryType);
        //    //}
        //    //return (IGenericRepositories<TEntity, TKey>)_repositories[type];
        //}
        public IGenericRepositories<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        => (IGenericRepositories<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepositories<TEntity, TKey>(_context)); 
            
        
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}  
