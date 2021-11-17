using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace Ereceipt.Web.AppSettings
{
    public class AuthenticationOption
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "2ed1d3417c4944928c098f1d72dbe57a93495c7ca2414527823d1dea5eae8184";
        public const int LIFETIME = 10;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}