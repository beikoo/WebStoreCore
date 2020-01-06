using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models.Interface;
using Repositories.Interfaces;
using System;
using System.Linq;

namespace Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class, IBaseModel, new()
    {
        public WebStoreDbContext Context { get; set; }
        public DbSet<T> DbSet { get; set; }

        public BaseRepository(WebStoreDbContext context)
        {
            this.Context = context ?? throw new ArgumentException("An instance of context is null");

            this.DbSet = this.Context.Set<T>();
        }

        public void Add(T entity)
        {
            EntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public IQueryable<T> All(Func<T, bool> func = null)
        {
            if (func == null)
            {
                return this.DbSet.AsQueryable<T>();
            }
            else
            {
                return this.DbSet.Where(func).AsQueryable<T>();
            }
        }

        public void Delete(T entity)
        {
            EntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public void Delete(object id)
        {
            var res = this.DbSet.Find(id);
            if (res != null)
            {
                this.Delete(res);
            }
        }

        public T GetByEntity(T entity)
        {
            var res = this.DbSet.Find(entity);
            if (res != null)
            {
                return res;
            }
            return null;
        }

        public T GetById(int id)
        {
            return this.DbSet.Find(id);
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }
        public virtual void Detach(T entity)
        {
            EntityEntry entry = this.Context.Entry(entity);

            entry.State = EntityState.Detached;
        }

        public void Update(T entity)
        {
            EntityEntry entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }
            entry.State = EntityState.Modified;
        }
    }
}
