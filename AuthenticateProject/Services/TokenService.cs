using AuthenticateProject.Options;
using AuthenticateProject.Services.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticateProject.EF.Models;
using AuthenticateProject.Repositories;

namespace AuthenticateProject.Services
{
    public class TokenService: ITokenService
    {
        private readonly BearerTokenOptions _options;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(
            IOptionsMonitor<BearerTokenOptions> options,
            ITokenRepository tokenRepository)
        {
            _options = options.CurrentValue;
            _tokenRepository = tokenRepository;
        }

        public async Task<TokenModel> GetTokenAsync(int userId,string mobileNumber)
        {
            var accessToken = GenerateJSONWebToken(mobileNumber);
            string refreshToken = GetUniqueKey();

            //prepare and add token in db
            var token = new Token()
            {
                UserId = userId,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpireToken = DateTime.Now.AddMinutes(_options.ExpireAccessToken),
                ExpireRefreshToken = DateTime.Now.AddMinutes(_options.ExpireRefreshToken),
            };
            await _tokenRepository.AddTokenAsync(token);
            await _tokenRepository.SaveChangesAsync();

            return new TokenModel()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GetUniqueKey()
        {
            var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var resultToken = new string(
               Enumerable.Repeat(allChar, 8)
               .Select(token => token[random.Next(token.Length)]).ToArray());

            return resultToken.ToString();
        }

        private string GenerateJSONWebToken(string mobileNumber)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, mobileNumber),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(_options.Issuer,
              _options.Audience,
              claims,
              expires: DateTime.Now.AddMinutes(_options.ExpireDate),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
