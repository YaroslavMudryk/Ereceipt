using Extensions.DeviceDetector.Models;

namespace Ereceipt.Application.ViewModels.Authentication
{
    public class SessionViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateUnActive { get; set; }
        public ClientInfo UnActiveFromDevice { get; set; }
        public ApplicationViewModel App { get; set; }
        public ClientInfo Device { get; set; }
        public LocationViewModel Location { get; set; }
    }
}
