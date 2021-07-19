using AuthenticateProject.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthProject.EF
{
    public class AuthContext:DbContext
    {
        public AuthContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<User> Users{ get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}
