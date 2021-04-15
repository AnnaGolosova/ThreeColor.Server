using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ThreeColor.IdentityService
{
    public class AuthOptions
    {
        public const string ISSUER = "ThreeColor.IdentityService"; 
        public const string AUDIENCE = "ThreeColor"; 
        const string KEY = "sdfsdfhns9jnfwiu4hf8obe";   
        public const int LIFETIME = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
