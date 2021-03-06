﻿using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Windows.Controls;
using MakeNotes.Framework.Controls;
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
        
        public Task ShowAsync<TView>() where TView : UserControl, new()
        {
            return ShowAsync<TView>(null, null);
        }
        
        public Task ShowAsync<TView>(object viewModel) where TView : UserControl, new()
        {
            return ShowAsync<TView>(viewModel, null);
        }
        
        public Task ShowAsync<TView>(object viewModel, DialogClosedEventHandler closedEventHandler) where TView : UserControl, new()
        {
            var view = GetOrCreateView<TView>();
            view.DataContext = viewModel;
            return DialogHost.Show(view, RootDialogIdentifier, (s, e) => OnCloseDialog(e, closedEventHandler));
        }

        public Task ShowConfirmationAsync(string title, string text, DialogClosedEventHandler closedEventHandler)
        {
            var parameters = new ConfirmationDialogParameters { Title = title, Text = text };
            return ShowAsync<ConfirmationDialog>(parameters, closedEventHandler);
        }

        class ConfirmationDialogParameters
        {
            public string Title { get; set; }
            public string Text { get; set; }
        }
    }
}
