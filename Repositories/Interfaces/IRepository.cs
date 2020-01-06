using Data;
using Microsoft.EntityFrameworkCore;
using Models.Interface;
using System;
using System.Linq;

namespace Repositories.Interfaces
{
    public interface IRepository<T> where T : class, IBaseModel
    {
        WebStoreDbContext Context { get; set; }
        DbSet<T> DbSet { get; set; }
        IQueryable<T> All(Func<T, bool> func = null);

        T GetById(int id);
        T GetByEntity(T entity);
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);
        int SaveChanges();

    }
}
