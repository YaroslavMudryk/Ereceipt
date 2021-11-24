using Ereceipt.Application.ViewModels.Authentication;
using Ereceipt.Domain.Models;

namespace Ereceipt.Application.ViewModels.Users
{
    public class UserLoginViewModel
    {
        public AuthViewModel Token { get; set; }

        public UserLoginViewModel(AuthViewModel token)
        {
            Token = token;
        }
    }
}