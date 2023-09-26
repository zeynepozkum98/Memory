using Memory.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Core.DataAccess
{
    public class RepositoryBase<T> : IRepository<T> where T : class, IEntity, new()
    {
        DbContext _dbContext;
        DbSet<T> _dbSet;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public Task<int> AddAsync(T entity)
        {
           _dbSet.Add(entity);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
            return _dbContext.SaveChangesAsync();
        }

        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            return expression == null ? _dbSet.Where(x => x.IsDeleted==false).ToListAsync():_dbSet.Where(expression)
                //Tamamını getirir elimize liste gelir 
                .Where(x=>x.IsDeleted==false).ToListAsync(); // silinmemiş olanların tamamını getirir
                
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
           return _dbSet.AsNoTracking().FirstOrDefaultAsync(expression); // takipten çıkması gerekiyor get yada getall a verebiliriz.
        }

        public Task<int> HardResetAsync(T entity)
        {
            _dbSet.Remove(entity);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(T entity)
        {
           _dbSet.Update(entity);
            return _dbContext.SaveChangesAsync();
        }
    }
}
