using MakeNotes.DAL.Infrastructure;
using SimpleMigrations;

namespace MakeNotes.DAL.Migrations
{
    [Migration(1, "Initial migration")]
    public class _0_Initial : SQLiteNetMigration
    {
        public override void Up()
        {
            Execute(
                @"CREATE TABLE [Tab]
                  (
                      [Id] INTEGER PRIMARY KEY,
                      [Name] TEXT NOT NULL,
                      [Order] INTEGER NOT NULL UNIQUE
                  )");

            Execute(
                @"CREATE TABLE [TabContent]
                  (
                      [Id] INTEGER PRIMARY KEY,
                      [TabId] INTEGER NOT NULL,
                      [Order] INTEGER NOT NULL,
                      [Data] TEXT NOT NULL,
                      FOREIGN KEY ([TabId]) REFERENCES [Tab]([Id]),
                      UNIQUE([TabId], [Order])
                  )");
        }

        public override void Down()
        {
            Execute("DROP TABLE [TabContent]");
            Execute("DROP TABLE [Tab]");
        }
    }
}
