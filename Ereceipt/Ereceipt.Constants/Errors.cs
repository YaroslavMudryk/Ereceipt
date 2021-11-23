using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ereceipt.Constants
{
    public static class Errors
    {
        public static class Users
        {
            public static string SomethingWrong = "Something went wrong";
            public static string UserAlreadyExist = "User already exist";
            public static string UserNotExist = "User not exist";
            public static string UserNotFound = "User not found";
            public static string UserAlreadyConfirmed = "User is already confirmed";
            public static string TokenExpired = "Time to confirm expired";
            public static string IncorrectToken = "Token is incorrect";
        }
    }

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
