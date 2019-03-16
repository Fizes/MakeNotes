using MakeNotes.Common.Core.Requests;

namespace MakeNotes.Notebook.Core.Commands
{
    public class AddPasswordSheet : IRequest<int>
    {
        public AddPasswordSheet(int tabContentId, string site, string username, string password, string description)
        {
            TabContentId = tabContentId;
            Site = site;
            Username = username;
            Password = password;
            Description = description;
        }

        public int TabContentId { get; }
        public string Site { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
    }
}
