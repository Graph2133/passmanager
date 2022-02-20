using System.Collections.Generic;

namespace PassManager.Core.VirtualDirectory
{
    /// <summary>
    /// The abstraction to store all passwords metadata
    /// and simplify reading of passwords. Index does not store
    /// any secrets or decrypted passwords. 
    /// </summary>
    public class IndexFile
    {
        public IndexFile()
        {
            this.Entries = new List<IndexEntry>();
        }

        public List<IndexEntry> Entries { get; set; }
    }
}
