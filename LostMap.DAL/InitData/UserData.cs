using LostMap.Common.Utils;
using LostMap.DAL.Models;

namespace LostMap.DAL.InitData
{
    public static class UserData
    {
        public static User Admin = new()
        {
            Id = new Guid("2DB91594-9C6B-4017-A21A-FC1854340F5A"),
            Name = "Admin",
            LastName = "Admin",
            Email = "admin@admin.com",
            PasswordHash = PasswordHasher.HashPassword("Qwer1234"),
            RoleId = RoleData.Ids.Admin
        };
    }
}
