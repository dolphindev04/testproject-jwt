using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticateProject.EF.Models
{
    public class Token:Base
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpireToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpireRefreshToken { get; set; }

        public User User { get; set; }
    }
}
