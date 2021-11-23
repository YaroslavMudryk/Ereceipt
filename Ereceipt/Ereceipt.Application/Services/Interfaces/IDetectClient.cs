using Ereceipt.Application.ViewModels.Users;
using Extensions.DeviceDetector.Models;

namespace Ereceipt.Application.Services.Interfaces
{
    public interface IDetectClient
    {
        ClientInfo GetClientInfo(DeviceCreateModel deviceModel);
    }
}
