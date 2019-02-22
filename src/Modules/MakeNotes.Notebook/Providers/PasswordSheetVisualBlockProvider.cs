using System.Threading.Tasks;
using MakeNotes.DAL.Core;
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
                @"INSERT INTO [PasswordSheet] ([Id], [Site], [Username], [Password], [Description])
                  VALUES (@Id, @Site, @Username, @Password, @Description);
                  SELECT last_insert_rowid()", visualBlock);

            return Repository.QuerySingleAsync<int>(query);
        }
    }
}
