using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    public class UsersRepository : BaseRepository<Person>, IUsersRepository
    {
        public UsersRepository(WebStoreDbContext context) : base(context)
        {
        }

        public Person GetByEmail(string email)
        {
            return this.DbSet.Where(u => u.Email == email).FirstOrDefault();
        }
    }
}
