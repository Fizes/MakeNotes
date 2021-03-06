﻿using System.Threading.Tasks;
using System.Windows.Controls;
using MakeNotes.Framework.Models;
using MakeNotes.Framework.Services;

namespace MakeNotes.IntegrationTests.Infrastructure.Fakes
{
    public class FakeInteractionService : IInteractionService
    {
        internal static DialogResult DialogResult { get; set; }

        public Task ShowAsync<TView>() where TView : UserControl, new()
        {
            return Task.CompletedTask;
        }

        public Task ShowAsync<TView>(object viewModel) where TView : UserControl, new()
        {
            return Task.CompletedTask;
        }

        public Task ShowAsync<TView>(object viewModel, DialogClosedEventHandler closedEventHandler) where TView : UserControl, new()
        {
            closedEventHandler?.Invoke(DialogResult);
            DialogResult = DialogResult.Unspecified;
            return Task.CompletedTask;
        }

        public Task ShowConfirmationAsync(string title, string text, DialogClosedEventHandler closedEventHandler)
        {
            closedEventHandler?.Invoke(DialogResult);
            DialogResult = DialogResult.Unspecified;
            return Task.CompletedTask;
        }
    }
}
