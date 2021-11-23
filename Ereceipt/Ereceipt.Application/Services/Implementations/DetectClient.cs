using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Application.ViewModels.Users;
using Extensions.DeviceDetector;
using Extensions.DeviceDetector.Models;

namespace Ereceipt.Application.Services.Implementations
{
    public class DetectClient : IDetectClient
    {
        private IDetector _detector;
        public DetectClient(IDetector detector)
        {
            _detector = detector;
        }

        public ClientInfo GetClientInfo(DeviceCreateModel deviceModel)
        {
            if (deviceModel.Device == null)
                return _detector.GetClientInfo();

            var device = deviceModel.Device;

            var clientInfo = new ClientInfo
            {
                Device = new Device
                {
                    Brand = device.Brand,
                    Model = device.Model,
                    Type = device.Type,
                },
                OS = new OS
                {
                    Name = device.OsName,
                    Platform = device.OsPlatform,
                    Version = device.OsVersion,
                },
                Browser = new Browser
                {
                    Name= device.BrowserName,
                    Version = device.BrowserVersion,
                    Engine = device.BrowserEngine,
                    EngineVersion = device.BrowserEngineVersion,
                    Type = device.BrowserType
                }
            };

            return clientInfo;
        }
    }
}
