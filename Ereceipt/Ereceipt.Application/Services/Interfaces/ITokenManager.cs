namespace Ereceipt.Application.Services.Interfaces
{
    public interface ITokenManager
    {
        bool IsAvailableToken(string token);
        bool AddNewToken(string token);
        bool AddNewTokens(IEnumerable<string> tokens);
        bool RemoveToken(string token);
        void RemoveAllTokens();
    }
}
