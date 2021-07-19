using AuthenticateProject.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticateProject.Services
{
    public interface ITokenService
    {
        Task<TokenModel> GetTokenAsync(int userId, string mobileNumber);
    }
}
