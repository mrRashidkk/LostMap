using LostMap.DAL.Models;

namespace LostMap.DAL.InitData
{
    public static class RoleData
    {
        public static readonly Role[] Roles = new[]
        {
            Create(Ids.Admin, Names.Admin),
            Create(Ids.User, Names.User)
        };

        private static Role Create(Guid id, string name) => new() { Id = id, Name = name };

        public static class Names
        {
            public const string Admin = nameof(Admin);
            public const string User = nameof(User);
        }

        public static class Ids
        {
            public static readonly Guid Admin = new("A1806BE9-F082-4A65-97D9-25F4E9A63C47");
            public static readonly Guid User = new("F1298354-3457-4283-AD8F-A6432D95EAA0");
        }
    }
}
