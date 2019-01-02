using System;

namespace MakeNotes.Notebook.Providers
{
    /// <summary>
    /// Provides global data of navigation region.
    /// </summary>
    public interface INavigationContext
    {
        /// <summary>
        /// Id of currently selected tab.
        /// </summary>
        int CurrentTabId { get; }
    }
}
