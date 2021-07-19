using AuthenticateProject.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticateProject.Repositories
{
    public interface IUserRepository
    {
        Task SaveChangesAsync();
    }
}
