namespace MakeNotes.Framework.Models
{
    /// <summary>
    /// Indicates with which result a dialog was closed.
    /// </summary>
    public enum DialogResult
    {
        /// <summary>
        /// A default value indicating that a dialog was closed normally.
        /// </summary>
        Unspecified = -1,
        /// <summary>
        /// Indicates that a dialog was closed with a negative outcome.
        /// </summary>
        Canceled = 0,
        /// <summary>
        /// Indicates that a dialog was closed with a positive outcome.
        /// </summary>
        Accepted = 1
    }
}
