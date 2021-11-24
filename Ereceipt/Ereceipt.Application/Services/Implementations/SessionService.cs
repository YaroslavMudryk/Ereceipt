using Ereceipt.Application.Services.Interfaces;
using Ereceipt.Application.ViewModels.Authentication;
using Ereceipt.Infrastructure.Data.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Ereceipt.Application.Services.Implementations
{
    public class SessionService : ISessionService
    {
        private readonly EreceiptContext _context;
        public SessionService(EreceiptContext context)
        {
            _context = context;
        }

        public async Task<Result<List<SessionViewModel>>> GetUserSessionsAsync(int userId)
        {
            var userSesisons = await _context.Sessions.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
            if (!userSesisons.Any())
                return new Result<List<SessionViewModel>>("Sessions not found");

            var sessionsToView = userSesisons.Select(x => new SessionViewModel
            {
                Id = x.Id,
                App = new ApplicationViewModel
                {
                    AppName = x.App.Name,
                    IsOfficial = x.App.IsOfficial,
                    Version = x.App.Version.ToString(),
                },
                CreatedAt = x.CreatedAt,
                DateUnActive = x.DateUnActive,
                Device = x.Device,
                IsActive = x.IsActive,
                UnActiveFromDevice = x.UnActiveFromDevice,
                Location = x.Location == null ? null : new LocationViewModel
                {
                    City = x.Location.City,
                    Country = x.Location.Country,
                    IP = x.Location.IP,
                    Lat = x.Location.Lat,
                    Lon = x.Location.Lon,
                    Region = x.Location.Region
                }
            }).ToList();

            return new Result<List<SessionViewModel>>(sessionsToView);
        }
    }
}
