using AuthenticateProject.EF.Models;
using AuthProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticateProject.Repositories
{
    public class TokenRepository:ITokenRepository
    {
        private readonly AuthContext _dbContext;

        public TokenRepository(AuthContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTokenAsync(Token token)
        {
            await _dbContext.Tokens.AddAsync(token);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
