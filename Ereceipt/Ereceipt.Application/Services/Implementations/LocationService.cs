using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Application.ViewModels.Locations;
using System.Text.Json;

namespace Ereceipt.Application.Services.Implementations
{
    public class LocationService : ILocationService
    {
        public async Task<IPGeo> GetIpInfoAsync(string ip)
        {
            using var httpClient = new HttpClient();
            var urlRequest = "http://ip-api.com/json";
            if (string.IsNullOrEmpty(ip))
            {
                urlRequest = urlRequest + "?fields=63700991";
            }
            else
            {
                if (ip.Contains("::1") || ip.Contains("localhost"))
                    urlRequest = urlRequest + "?fields=63700991";
                else
                    urlRequest = urlRequest + $"/{ip}?fields=63700991";
            }
            var resultFromApi = await httpClient.GetAsync(urlRequest);
            if (!resultFromApi.IsSuccessStatusCode)
                return null;
            var content = await resultFromApi.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IPGeo>(content);
        }
    }
}
