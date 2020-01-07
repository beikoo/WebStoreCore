using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class WebStoreDbContext:DbContext
    {
        public WebStoreDbContext()
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesOrder> Sales { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //localhost\SQLEXPRESS01
           // "server=localhost\SQLEXPRESS01;database=WebStoreCore;trusted_connection=true;"
            optionsBuilder.UseSqlServer(@"Server=airfan\SQLEXPRESS;Database=WebApiCore1;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product() { Id = 1, Name="Iphone",Quantity=300,CreatedAt = DateTime.Now,Description ="Iphone 7 128GB" });

            
        }
        public override int SaveChanges()
        {
            this.ApplyEntityChanges();
            return base.SaveChanges();
        }

        private void ApplyEntityChanges()
        {
            var entries = this.ChangeTracker.Entries().Where(x => x.Entity is BaseModel && x.State == EntityState.Added || x.State == EntityState.Deleted || x.State == EntityState.Modified).ToList();

            foreach (var entry in entries)
            {
                var entity = (BaseModel)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.Now;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entity.DeletedAt = DateTime.Now;

                }
                else
                {
                    entity.ModifiedAt = DateTime.Now;
                }
            }
        }
    }
}
