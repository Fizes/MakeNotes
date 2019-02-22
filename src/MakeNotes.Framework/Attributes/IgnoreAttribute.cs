using System;

namespace MakeNotes.Framework.Attributes
{
    /// <summary>
    /// Specifies that a data field should not be generated automatically on UI.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreAttribute : Attribute
    {
    }
}
