using Ereceipt.Application.ViewModels.Locations;

namespace Ereceipt.Application.Services.Interfaces
{
    public interface ILocationService
    {
        Task<IPGeo> GetIpInfoAsync(string ip);
    }
}
