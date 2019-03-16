using MakeNotes.Common.Core.Requests;

namespace MakeNotes.Notebook.Core.Commands
{
    public class UpdatePasswordSheet : IRequest
    {
        public UpdatePasswordSheet(int id, string site, string username, string password, string description)
        {
            Id = id;
            Site = site;
            Username = username;
            Password = password;
            Description = description;
        }
        
        public int Id { get; }
        public string Site { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
    }
}
