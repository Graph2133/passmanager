using System.Collections.Generic;

namespace PassManager.Core.VirtualDirectory
{
    /// <summary>
    /// The metadata represenation of saved password entry.
    /// </summary>
    public class IndexEntry
    {
        public IndexEntry()
        {
            this.Tags = new List<string>();
        }

        public string Name { get; set; }

        public string Filename { get; set; }

        public bool HasCustomSecret { get; set; }

        public List<string> Tags { get; set; }
    }
}
