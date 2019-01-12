namespace MakeNotes.DAL.Models
{
    /// <summary>
    /// Contains possible types of visual blocks.
    /// </summary>
    public class VisualBlockType
    {
        /// <summary>
        /// Unique id.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// System name of the visual block that is used to find the appropriate entity.
        /// </summary>
        public string SysName { get; set; }
    }
}
