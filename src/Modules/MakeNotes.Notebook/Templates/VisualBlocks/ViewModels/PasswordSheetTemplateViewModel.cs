using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MakeNotes.Common.Core;
using MakeNotes.Framework.Models;
using MakeNotes.Framework.Services;
using MakeNotes.Framework.Validation;
using MakeNotes.Notebook.Core.Commands;
using MakeNotes.Notebook.Core.Notifications;
using MakeNotes.Notebook.Core.Queries;
using MakeNotes.Notebook.Properties;
using MakeNotes.Notebook.Templates.VisualBlocks.Models;
using Prism.Commands;

namespace MakeNotes.Notebook.Templates.VisualBlocks.ViewModels
{
    public class PasswordSheetTemplateViewModel : VisualBlockViewModelBase<PasswordSheetDto>
    {
        private readonly IMessageBus _messageBus;
        private readonly IInteractionService _interactionService;

        private string _title = "Passwords";

        public PasswordSheetTemplateViewModel(IMessageBus messageBus, IInteractionService interactionService)
        {
            _messageBus = messageBus;
            _interactionService = interactionService;

            AddItemCommand = new DelegateCommand(AddItem);
            SavetemCommand = new DelegateCommand<PasswordSheetDto>(SaveItem);
            DeleteItemCommand = new DelegateCommand<PasswordSheetDto>(DeleteItem);
            DeleteCommand = new DelegateCommand(Delete);
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ICommand AddItemCommand { get; }

        public ICommand SavetemCommand { get; }

        public ICommand DeleteItemCommand { get; }

        public ICommand DeleteCommand { get; }

        protected async override void Initialize(int tabContentId)
        {
            var sheets = await _messageBus.SendAsync(new GetPasswordSheetsByTabContentId(tabContentId));
            Items.AddRange(sheets);
        }

        private void AddItem()
        {
            Items.Add(new PasswordSheetDto());
        }

        private async void SaveItem(PasswordSheetDto item)
        {
            if (!ModelValidator.Validate(item))
            {
                return;
            }

            if (item.Id.HasValue)
            {
                var request = new UpdatePasswordSheet(item.Id.Value, item.Site, item.Username, item.Password, item.Description);
                await _messageBus.SendAsync(request);
            }
            else
            {
                var request = new AddPasswordSheet(TabContentId, item.Site, item.Username, item.Password, item.Description);
                item.Id = await _messageBus.SendAsync(request);
            }
        }

        private async void DeleteItem(PasswordSheetDto item)
        {
            if (item.Id.HasValue)
            {
                await _messageBus.SendAsync(new DeletePasswordSheet(item.Id.Value, TabContentId));
            }

            Items.Remove(item);
        }

        private async void Delete()
        {
            await _interactionService.ShowConfirmationAsync(
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
