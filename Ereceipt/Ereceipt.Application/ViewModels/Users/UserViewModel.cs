namespace Ereceipt.Application.ViewModels.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public string About { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}