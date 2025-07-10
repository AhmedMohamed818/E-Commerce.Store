using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepositories<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Task <int> CountAsync(ISpecifications<TEntity, TKey> spec );
        // Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications< TEntity, TKey> specifications, bool trackChanges = false);

        Task <TEntity?> GetAsync(int id);
        Task<TEntity?> GetAsync(ISpecifications<TEntity, TKey> specifications, bool trackChanges = false);

        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
   
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
    
}
