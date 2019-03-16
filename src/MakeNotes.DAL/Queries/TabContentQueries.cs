namespace MakeNotes.DAL.Queries
{
    public static class TabContentQueries
    {
        public const string GetTabContentByTabId =
            @"SELECT [Id], [TabId], [Order], [VisualBlockTypeId]
              FROM [TabContent]
              WHERE [TabId] = @TabId
              ORDER BY [Order];";
    }
}
