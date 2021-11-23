using System.Security.Claims;

namespace Ereceipt.Application.ViewModels.Users
{
    public class AuthenticationViewModel
    {
        public List<Claim> Claims { get; set; }
        public string Error { get; set; }
    }
}