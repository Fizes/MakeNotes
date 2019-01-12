using MakeNotes.DAL.Infrastructure;
using SimpleMigrations;

namespace MakeNotes.DAL.Migrations
{
    [Migration(2019_01_06_14_00)]
    public class Add_TabContent_Forms : SQLiteNetMigration
    {
        public Add_TabContent_Forms()
        {
            UseTransaction = true;
        }

        public override void Up()
        {
            Execute(
                $@"CREATE TABLE [PasswordSheet]
                  (
                      [Id] INTEGER PRIMARY KEY,
                      [Site] TEXT NULL,
                      [Username] TEXT NOT NULL,
                      [Password] TEXT NOT NULL,
                      [Description] TEXT NULL
                  )");

            Execute(
                $@"CREATE TABLE [VisualBlockType]
                  (
                      [Id] INTEGER PRIMARY KEY,
                      [SysName] TEXT NOT NULL UNIQUE
                  );

                  INSERT INTO [VisualBlockType] ([SysName])
                  VALUES ('PasswordSheet')");
            
            Execute(
                @"DROP TABLE [TabContent];

                  CREATE TABLE [TabContent]
                  (
                      [Id] INTEGER PRIMARY KEY,
                      [TabId] INTEGER NOT NULL,
                      [VisualBlockTypeId] INTEGER NOT NULL,
                      [Order] INTEGER NOT NULL,
                      FOREIGN KEY ([TabId]) REFERENCES [Tab]([Id]),
                      FOREIGN KEY ([VisualBlockTypeId]) REFERENCES [VisualBlockType]([Id]),
                      UNIQUE([TabId], [Order])
                  )");

            Execute(
                $@"CREATE TABLE [TabContentVisualBlock]
                  (
                      [Id] INTEGER PRIMARY KEY,
                      [TabContentId] INTEGER NOT NULL,
                      [VisualBlockId] INTEGER NOT NULL,
                      FOREIGN KEY ([TabContentId]) REFERENCES [TabContent]([Id]),
                      UNIQUE([TabContentId], [VisualBlockId])
                  )");
        }

        public override void Down()
        {
            Execute("DROP TABLE [TabContentVisualBlock]");
            Execute("DROP TABLE [VisualBlockType]");
            Execute("DROP TABLE [PasswordSheet]");
            Execute(
                @"DROP TABLE [TabContent];

                  CREATE TABLE [TabContent]
                  (
                      [Id] INTEGER PRIMARY KEY,
                      [TabId] INTEGER NOT NULL,
                      [Order] INTEGER NOT NULL,
                      [Data] TEXT NOT NULL,
                      FOREIGN KEY ([TabId]) REFERENCES [Tab]([Id]),
                      UNIQUE([TabId], [Order])
                  )");
        }
    }
}
