using System.Text.Json;
namespace Ereceipt.Infrastructure.Data.EntityFramework.Configurations
{
    public static class ConfigurationExtensions
    {
        public static string ToJson(this object data)
        {
            return JsonSerializer.Serialize(data);
        }

        public static T FromJson<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}