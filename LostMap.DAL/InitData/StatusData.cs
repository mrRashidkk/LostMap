using LostMap.DAL.Models;

namespace LostMap.DAL.InitData
{
    public static class StatusData
    {
        public static readonly Status[] Statuses = new Status[]
        {
            Create(Ids.Active, Names.Active),
            Create(Ids.WaitingForAuthor, Names.WaitingForAuthor),
            Create(Ids.Closed, Names.Closed)
        };

        private static Status Create(Guid id, string name) => new() { Id = id, Name = name };

        public static class Ids
        {
            public static readonly Guid Active = new("B3FED4BE-85E9-4F70-AEB8-2797A153F813");
            public static readonly Guid WaitingForAuthor = new("9E1753C2-9850-40E8-B092-82282711E006");
            public static readonly Guid Closed = new("87874DEF-44E8-46D5-AF66-8F7A64559150");
        }

        public static class Names
        {
            public const string Active = "Active";
            public const string WaitingForAuthor = "Waiting for author";
            public const string Closed = "Closed";
        }
    }
}
