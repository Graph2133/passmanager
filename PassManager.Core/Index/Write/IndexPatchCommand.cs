using PassManager.Core.VirtualDirectory;
using System.Collections.Generic;
using System.Linq;

namespace PassManager.Core.Index.Write
{
    /// <summary>
    /// A wrapper around <see cref="IndexEntry"/> to incapsulate update logic.
    /// </summary>
    public class IndexPatchCommand
    {
        private readonly IndexEntry entry;

        public IndexPatchCommand(IndexEntry entry)
        {
            this.entry = entry;
        }

        public void SetName(string name)
        {
            entry.Name = name;
        }

        public void SetTags(IEnumerable<string> tags)
        {
            entry.Tags = tags.ToList();
        }

        public void SetHasCustomSecret(bool hasCustomSecret)
        {
            entry.HasCustomSecret = hasCustomSecret;
        }
    }
}
