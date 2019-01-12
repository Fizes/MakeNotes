using MakeNotes.Common.Models;

namespace MakeNotes.DAL.Models
{
    /// <summary>
    /// Represents an item of a password sheet.
    /// </summary>
    public class PasswordSheet : IVisualBlock
    {
        /// <summary>
        /// Unique id associated with a password sheet.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Site to which the password is related (e.g. website, program, etc.).
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Username that is used to login the site.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password that is used to login the site.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Optional description.
        /// </summary>
        public string Description { get; set; }
    }
}
