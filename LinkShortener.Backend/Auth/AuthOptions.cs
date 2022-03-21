using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.Backend.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";        // Видатель токену
        public const string AUDIENCE = "MyAuthClient";      // Споживач токену
        private const string KEY = "mysupersecret_secretkey!123";   // Ключ для шифрування
        public const int LIFETIME = 180;                    // Час життя токену (хв), 3 години
        
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
