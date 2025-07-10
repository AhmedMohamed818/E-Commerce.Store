using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        // Generate Repository
        IGenericRepositories<TEntity, TKey> GetRepository<TEntity , TKey>() where TEntity : BaseEntity<TKey>;
        //Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        //Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess);
        //Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);
        //void Dispose();
    }
}
