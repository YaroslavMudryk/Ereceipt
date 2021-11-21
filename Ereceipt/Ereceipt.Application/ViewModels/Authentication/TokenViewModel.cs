namespace Ereceipt.Application.ViewModels.Authentication
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public string Type { get; set; }
        public DateTime ExpiredAt { get; set; }
        public List<ClaimViewModel> Claims { get; set; }
    }
}
