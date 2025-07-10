using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class GenericRepositories<TEntity, TKey> : IGenericRepositories<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepositories(StoreDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return trackChanges ?
                    await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync() as IEnumerable<TEntity>
                  : await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }
            return trackChanges ?
                  await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().AsNoTracking().ToListAsync();



            //if (trackChanges) await _context.Set<TEntity>().ToListAsync();
            //    return await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        }

        public async Task<TEntity?> GetAsync(int id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync(p => p.Id == id as int?) as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }


        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);

        }
        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Remove(entity);

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications, bool trackChanges = false)
        {
            return await ApplySpecification(specifications).ToListAsync();

        }

        public async Task<TEntity?> GetAsync(ISpecifications<TEntity, TKey> specifications, bool trackChanges = false)
        {
            return await ApplySpecification(specifications).FirstOrDefaultAsync();

        }
        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        private IQueryable<TEntity> ApplySpecification(ISpecifications<TEntity, TKey> specifications)
        {

            return SpecificationEvaluator.GetQuery(_context.Set<TEntity>(), specifications);
        }

      
    }
}
