using System;
using System.Windows.Input;
using MakeNotes.Common.Core;
using MakeNotes.DAL.Models;
using MakeNotes.Framework.Models;
using MakeNotes.Framework.Services;
using MakeNotes.Notebook.Core.Notifications;
using MakeNotes.Notebook.Properties;
using MakeNotes.Notebook.Templates.VisualBlocks.Models;
using Prism.Commands;

namespace MakeNotes.Notebook.Templates.VisualBlocks.ViewModels
{
    public class PasswordSheetTemplateViewModel : VisualBlockViewModelBase<PasswordSheet>
    {
        private readonly IMessageBus _messageBus;
        private readonly IInteractionService _interactionService;

        private string _title = "Passwords";

        public PasswordSheetTemplateViewModel(IMessageBus messageBus, IInteractionService interactionService)
        {
            _messageBus = messageBus;
            _interactionService = interactionService;

            AddItemCommand = new DelegateCommand(AddItem);
            DeleteCommand = new DelegateCommand(Delete);
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ICommand AddItemCommand { get; }

        public ICommand DeleteCommand { get; }

        protected override void Initialize(int tabContentId)
        {
        }

        private void AddItem()
        {
            Items.Add(new PasswordSheet());
        }

        private async void Delete()
        {
            await _interactionService.ShowConfirmation(
                String.Format(Resources.DeletePasswordsTitle, Title),
                Resources.DeletePasswordsText,
                OnCloseDeleteDialog);
        }

        private void OnCloseDeleteDialog(DialogResult result)
        {
            if (result == DialogResult.Accepted)
            {
                _messageBus.Publish(new TabContentDeleted(TabContentId));
            }
        }
    }
}
