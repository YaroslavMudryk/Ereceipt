using Ereceipt.Application.ViewModels.Authentication;
namespace Ereceipt.Application.Services.Interfaces
{
    public interface ISessionService
    {
        Task<Result<List<SessionViewModel>>> GetUserSessionsAsync(int userId);
    }
}