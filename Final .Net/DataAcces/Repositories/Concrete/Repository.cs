using Core.Entities.Base;
using DataAcces.Contexts;
using DataAcces.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbset;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbset = appDbContext.Set<T>();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }
        public async Task CreateAsync(T entity)
        {
            await _dbset.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbset.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            _dbset.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbset.AnyAsync(predicate);
        }

        public async Task SaveChanges()
        {
           await _appDbContext.SaveChangesAsync();
        }

        public async Task<T> FirstorDefaultAsync()
        {
           return await _dbset.FirstOrDefaultAsync();
        }
    }
}
