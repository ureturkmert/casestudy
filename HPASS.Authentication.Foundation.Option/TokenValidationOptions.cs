using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Authentication.Foundation.Option
{
    public class TokenValidationOptions
    {

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public bool ValidateSignKey { get; set; }

        public bool ValidateIssuer { get; set; }

        public bool ValidateAudience { get; set; }

        public bool ValidateLifetime { get; set; }

        public TokenValidationOptions()
        {
            this.Issuer = null;
            this.Audience = null;
            this.ValidateSignKey = true;
            this.ValidateIssuer = false;
            this.ValidateAudience = false;
            this.ValidateLifetime = true;
        }
    }
}
