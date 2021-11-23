namespace Ereceipt.Application.ViewModels.Users
{
    public class DeviceInfoModel
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }

        public string OsName { get; set; }
        public string OsPlatform { get; set; }
        public string OsVersion { get; set; }

        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        public string BrowserEngine { get; set; }
        public string BrowserEngineVersion { get; set; }
        public string BrowserType { get; set; }
    }
}