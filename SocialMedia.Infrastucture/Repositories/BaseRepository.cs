using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastucture.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T:BaseEntiy
    {
        private readonly SocialMediaContext _context;
        private DbSet<T> _entities;
        public BaseRepository(SocialMediaContext context)
        {
            _context = context; 
            _entities = context.Set<T>();//se genera  un dbSet
        }
        public async Task Add(T Entity)
        {
             _entities.Add(Entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entities = await GetById(id);
            _entities.Remove(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Update(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();

        }
    }
}
