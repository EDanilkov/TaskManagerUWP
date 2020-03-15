using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ServerAPI.Authorization
{
    public class AuthOptions
    {
        public const string Issuer = "ServerAPI";
        public const string Audience = "http://localhost:44393/";
        const string Key = "secretkeyforServerAPI"; 
        public const int LifeTime = 1; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
