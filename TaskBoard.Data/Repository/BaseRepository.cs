using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskBoard.Data.Context;
using TaskBoard.Domain.Model;
using TaskBoard.Domain.Repository;

namespace TaskBoard.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly TaskBoardDbContext context;

        public BaseRepository(TaskBoardDbContext context)
        {
            this.context = context;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            var existingEntity = await context.Set<T>().FindAsync(entity.Id); // maybe better to check for null or empty Guid than db hit. Data integrity first, performance second

            if (existingEntity != null)
            {
                context.Entry(existingEntity).CurrentValues.SetValues(entity);
                context.Set<T>().Update(existingEntity);
            }
            else
            {
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with Id {entity.Id} not found.");
            }
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await context.Set<T>().AddRangeAsync(entities);
        }
        public async Task DeleteAsync(Guid id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
            }
            else
            {
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with Id {id} not found.");
            }
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }    
    }
}
