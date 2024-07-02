using Data.Contexts;
using Data.Contracts;
using Entities;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly ApplicationDbContext _context;
        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
        public Repository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            Entities = _context.Set<TEntity>();
        }
        public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return Entities.FindAsync(ids, cancellationToken);
        }
        public virtual async Task Add(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            if (entity is not null)
                Entities.Add(entity);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            if (entities is not null)
                await Entities.AddRangeAsync(entities, cancellationToken);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            if (entity is not null)
                Entities.Update(entity);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            if (entities is not null)
                Entities.UpdateRange(entities);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            if (entity is not null)
                Entities.Remove(entity);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            if (entities is not null)
                Entities.RemoveRange(entities);
            if (saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
