using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticateProject.Options
{
    public class BearerTokenOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireAccessToken { get; set; }
        public int ExpireRefreshToken { get; set; }
    }
}
