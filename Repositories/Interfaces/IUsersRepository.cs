using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Interfaces
{
    public interface IUsersRepository : IRepository<Person>
    {
        Person GetByEmail(string email);
    }
}
