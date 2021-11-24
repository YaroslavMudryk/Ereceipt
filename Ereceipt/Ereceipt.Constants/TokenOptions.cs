using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ereceipt.Constants
{
    public class TokenOptions
    {
        public const string ISSUER = "Ereceipt";
        public const string AUDIENCE = "EreceiptClient";
        const string KEY = "fad4537e75f24c17b2940caaef407f2530fe372acf8c47fbb36f8fb91c4c74a3";
        public const int LIFETIME = 15;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
