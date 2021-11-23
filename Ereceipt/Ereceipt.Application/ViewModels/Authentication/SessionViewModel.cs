namespace Ereceipt.Application.ViewModels.Authentication
{
    public class SessionViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Token { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateUnactive { get; set; }
        public Dictionary<string, List<string>> Claims { get; set; }
        public DeviceViewModel Device { get; set; }
        public LocationViewModel Location { get; set; }
        public ApplicationViewModel Application { get; set; }
    }
}
