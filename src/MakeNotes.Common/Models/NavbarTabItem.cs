using System;

namespace MakeNotes.Common.Models
{
    /// <summary>
    /// Represents a tab item inside the navbar.
    /// </summary>
    public class NavbarTabItem
    {
        public NavbarTabItem()
        {
            Id = Guid.NewGuid();
        }

        public NavbarTabItem(string header, int order) : this()
        {
            Header = header;
            Order = order;
        }

        /// <summary>
        /// Unique id associated with a particular tab.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Tab title.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Zero-based index of the tab inside the navbar.
        /// </summary>
        public int Order { get; set; }
    }
}
