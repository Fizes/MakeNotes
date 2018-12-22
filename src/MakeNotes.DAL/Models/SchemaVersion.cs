using System;

namespace MakeNotes.DAL.Models
{
    /// <summary>
    /// Represents a table that stores a list of applied migrations. Used by Simple.Migrations.
    /// </summary>
    public class SchemaVersion
    {
        public int Id { get; set; }
        public long Version { get; set; }
        public DateTime AppliedOn { get; set; }
        public string Description { get; set; }
    }
}
