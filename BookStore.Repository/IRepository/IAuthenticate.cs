using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository.IRepository
{
    public interface IAuthenticate<TEntity>
    {
        TEntity AuthenticateUser(string username, string password);
    }
}
