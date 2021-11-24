using Ereceipt.Domain.Models;

namespace Ereceipt.Application.ViewModels.Users
{
    public class TokenDataViewModel
    {
        public User User { get; set; }
        public UserLogin UserLogin { get; set; }
        public App App { get; set; }
        public List<Role> Roles { get; set; }
        public Session Session { get; set; }
    }
}