using System;

namespace MakeNotes.Common.Interfaces
{
    /// <summary>
    /// Executes an operation on a separate thread.
    /// </summary>
    public interface IBackgroundTask
    {
        /// <summary>
        /// Starts execution of a background operation.
        /// </summary>
        /// <param name="action">Background operation to be executed.</param>
        void Start(Action action);
    }
}
