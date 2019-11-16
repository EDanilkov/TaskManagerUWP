using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ServerAPI.Authorization
{
    public class AuthOptions
    {
        public const string ISSUER = "ServerAPI";
        public const string AUDIENCE = "http://localhost:44393/";
        const string KEY = "secretkeyforServerAPI"; 
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
