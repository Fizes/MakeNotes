using System.Threading.Tasks;
using MakeNotes.DAL.Core;
using MakeNotes.DAL.Queries;
using MakeNotes.Notebook.Consts;
using MakeNotes.Notebook.Templates.VisualBlocks.Models;

namespace MakeNotes.Notebook.Providers
{
    public class PasswordSheetVisualBlockProvider : TabContentVisualBlockProviderBase<PasswordSheetDto>
    {
        public PasswordSheetVisualBlockProvider(IRepository repository) : base(repository)
        {
        }

        public override string SysName => VisualBlockTypes.PasswordSheet;

        protected override Task<int> CreateVisualBlockAsync(PasswordSheetDto visualBlock)
        {
            var query = new QueryObject(
                @"INSERT INTO [PasswordSheet] ([Site], [Username], [Password], [Description])
                  VALUES (@Site, @Username, @Password, @Description);" +
                  CommonQueries.GetInsertedId, visualBlock);

            return Repository.QuerySingleAsync<int>(query);
        }

        public override Task UpdateVisualBlockAsync(PasswordSheetDto visualBlock)
        {
            var query = new QueryObject(
                @"UPDATE [PasswordSheet]
                  SET [Site] = @Site,
                      [Username] = @Username,
                      [Password] = @Password,
                      [Description] = @Description
                  WHERE [Id] = @Id", visualBlock);

            return Repository.ExecuteAsync(query);
        }
    }
}
