using System.Threading.Tasks;
using System.Windows.Controls;
using MakeNotes.Framework.Models;
using MakeNotes.Framework.Services;

namespace MakeNotes.IntegrationTests.Infrastructure.Fakes
{
    public class FakeInteractionService : IInteractionService
    {
        internal static DialogResult DialogResult { get; set; }

        public Task Show<TView>() where TView : UserControl, new()
        {
            return Task.CompletedTask;
        }

        public Task Show<TView>(object viewModel) where TView : UserControl, new()
        {
            return Task.CompletedTask;
        }

        public Task Show<TView>(object viewModel, DialogClosedEventHandler closedEventHandler) where TView : UserControl, new()
        {
            closedEventHandler?.Invoke(DialogResult);
            DialogResult = DialogResult.Unspecified;
            return Task.CompletedTask;
        }
    }
}
