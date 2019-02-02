using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Windows.Controls;
using MakeNotes.Framework.Models;
using MaterialDesignThemes.Wpf;

namespace MakeNotes.Framework.Services
{
    public class InteractionService : IInteractionService
    {
        private const string RootDialogIdentifier = "RootDialogArea";

        // Makes an attempt to parse the value either as DialogResult or bool
        private DialogResult ParseDialogResult(string value)
        {
            if (bool.TryParse(value, out bool parsedBoolValue))
            {
                return (DialogResult)Convert.ToInt32(parsedBoolValue);
            }

            if (Enum.TryParse(value, out DialogResult parsedEnumValue))
            {
                return parsedEnumValue;
            }

            return DialogResult.Unspecified;
        }

        // Parses the event parameter and invokes the handler with passing the parsed parameter to it
        private void OnCloseDialog(DialogClosingEventArgs e, DialogClosedEventHandler closedEventHandler)
        {
            var parameter = e.Parameter?.ToString();
            var dialogResult = ParseDialogResult(parameter);
            closedEventHandler?.Invoke(dialogResult);
        }

        // Caches a view to reuse it without performance hit
        private TView GetOrCreateView<TView>() where TView : UserControl, new()
        {
            var cache = MemoryCache.Default;
            var key = typeof(TView).FullName;
            var view = cache.Get(key) as TView;

            if (view == null)
            {
                view = Activator.CreateInstance<TView>();
                cache.Add(key, view, absoluteExpiration: DateTime.MaxValue);
            }

            return view;
        }
        
        public async Task Show<TView>() where TView : UserControl, new()
        {
            await Show<TView>(null, null);
        }
        
        public async Task Show<TView>(object viewModel) where TView : UserControl, new()
        {
            await Show<TView>(viewModel, null);
        }
        
        public async Task Show<TView>(object viewModel, DialogClosedEventHandler closedEventHandler) where TView : UserControl, new()
        {
            var view = GetOrCreateView<TView>();
            view.DataContext = viewModel;
            await DialogHost.Show(view, RootDialogIdentifier, (s, e) => OnCloseDialog(e, closedEventHandler));
        }
    }
}
