using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class UserTokenRepository : BaseRepository<UserToken>, IUserTokensRepository
    {
        public UserTokenRepository(WebStoreDbContext context) : base(context)
        {

        }
    }
}
