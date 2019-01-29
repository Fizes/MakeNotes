namespace MakeNotes.DAL.Queries
{
    public static class TabQueries
    {
        public const string CreateTab =
            @"INSERT INTO [Tab] ([Name], [Order])
              VALUES(@Name, @Order);";

        public const string DeleteTab =
            @"DELETE FROM [Tab]
              WHERE [Id] = @Id;";
        
        public const string GetAllTabs =
            @"SELECT [Id], [Name], [Order]
              FROM [Tab]
              ORDER BY [Order]";

        public const string FindTabById =
            @"SELECT [Id], [Name], [Order]
              FROM [Tab]
              WHERE [Id] = @Id";

        public const string GetLastTabOrder =
            @"SELECT COALESCE(MAX([Order]), 0)
              FROM [Tab]";
    }
}
