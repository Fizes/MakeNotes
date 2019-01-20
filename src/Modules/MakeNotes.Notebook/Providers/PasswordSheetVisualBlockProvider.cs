using System.Threading.Tasks;
using MakeNotes.DAL.Core;
using MakeNotes.DAL.Models;
using MakeNotes.Notebook.Consts;

namespace MakeNotes.Notebook.Providers
{
    public class PasswordSheetVisualBlockProvider : TabContentVisualBlockProviderBase<PasswordSheet>
    {
        public PasswordSheetVisualBlockProvider(IRepository repository) : base(repository)
        {
        }

        public override string SysName => VisualBlockTypes.PasswordSheet;

        protected override Task<int> CreateVisualBlockAsync(PasswordSheet visualBlock)
        {
            var query = new QueryObject(
                @"INSERT INTO [PasswordSheet] ([Id], [Site], [Username], [Password], [Description])
                  VALUES (@Id, @Site, @Username, @Password, @Description);
                  SELECT last_insert_rowid()", visualBlock);

            return Repository.QuerySingleAsync<int>(query);
        }
    }
}
