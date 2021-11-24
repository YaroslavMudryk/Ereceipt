using Ereceipt.Constants;
using Ereceipt.Domain.Models;

namespace Ereceipt.Infrastructure.Data
{
    public static class AppsFactory
    {
        public static List<App> GetApps()
        {
            var apps = new List<App>();

            apps.Add(new App
            {
                ActiveFrom = DateTime.Now,
                ActiveTill = DateTime.Now.AddYears(5),
                CanUseWhileDevelopment = Enumerable.Range(1, 5).ToArray(),
                InDevelopment = true,
                CreatedAt = DateTime.Now,
                CreatedBy = CreatedConsts.CreatedByDefault,
                CreatedFromIP = CreatedConsts.CreatedFromIpDefault,
                AppId = "XFPY-HQJH-BWED-97SI-ISCW",
                AppSecret = "b0iggum7okttfqsjuobexvqvnxk5x355gzleitkxgbruvj5wib",
                IsOfficial = true,
                Version = 0,
                CompanyUrl = "https://ereceipt.com.ua",
                Description = "Official web application of ereceipt system",
                Name = "Ereceipt Web",
                ShortName = "Ereceipt Web"
            });

            apps.Add(new App
            {
                ActiveFrom = DateTime.Now,
                ActiveTill = DateTime.Now.AddYears(5),
                CanUseWhileDevelopment = Enumerable.Range(1, 5).ToArray(),
                InDevelopment = true,
                CreatedAt = DateTime.Now,
                CreatedBy = CreatedConsts.CreatedByDefault,
                CreatedFromIP = CreatedConsts.CreatedFromIpDefault,
                AppId = "XFPY-HQJH-BWED-97SI-ISCW",
                AppSecret = "b0iggum7okttfqsjuobexvqvnxk5x355gzleitkxgbruvj5wib",
                IsOfficial = true,
                Version = 0,
                CompanyUrl = "https://ereceipt.com.ua",
                Description = "Testing web application of ereceipt system",
                Name = "Ereceipt Web Test",
                ShortName = "Ereceipt Web Test"
            });

            return apps;
        }
    }
}
