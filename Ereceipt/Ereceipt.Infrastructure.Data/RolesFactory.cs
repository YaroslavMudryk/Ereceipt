using Ereceipt.Constants;
using Ereceipt.Domain.Models;

namespace Ereceipt.Infrastructure.Data
{
    public static class RolesFactory
    {
        public static Role GetAdminRole()
        {
            return new Role
            {
                Name = RolesConstants.Administrator,
                SystemName = RolesConstants.Administrator.ToUpper(),
                CreatedAt = DateTime.Now,
                CreatedBy = CreatedConsts.CreatedByDefault,
                CreatedFromIP = CreatedConsts.CreatedFromIpDefault,
                Lvl = 999,
                Version = 0
            };
        }

        public static Role GetUserRole()
        {
            return new Role
            {
                Name = RolesConstants.User,
                SystemName = RolesConstants.User.ToUpper(),
                CreatedAt = DateTime.Now,
                CreatedBy = CreatedConsts.CreatedByDefault,
                CreatedFromIP = CreatedConsts.CreatedFromIpDefault,
                Lvl = 1,
                Version = 0
            };
        }

        public static Role GetModeratorRole()
        {
            return new Role
            {
                Name = RolesConstants.Moderator,
                SystemName = RolesConstants.Moderator.ToUpper(),
                CreatedAt = DateTime.Now,
                CreatedBy = CreatedConsts.CreatedByDefault,
                CreatedFromIP = CreatedConsts.CreatedFromIpDefault,
                Lvl = 2,
                Version = 0
            };
        }

        public static IEnumerable<Role> GetAllRoles()
        {
            yield return GetAdminRole();
            yield return GetModeratorRole();
            yield return GetUserRole();
        }
    }
}
