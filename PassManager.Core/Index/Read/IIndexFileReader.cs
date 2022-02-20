using PassManager.Core.VirtualDirectory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassManager.Core.Index.Read
{
    /// <summary>
    /// The index file reader. Reader decrypts and deserializes index entries from BSON format.
    /// </summary>
    public interface IIndexFileReader
    {
        /// <summary>
        /// Get the entry from index file by name.
        /// </summary>
        /// <param name="name">The name of index entry.</param>
        /// <returns>A index entry.</returns>
        Task<IndexEntry> GetByNameAsync(string name);

        /// <summary>
        /// Gets index entries by query.
        /// </summary>
        /// <param name="query">The query to get index entries.</param>
        /// <returns>A list of index entries.</returns>
        Task<List<IndexEntry>> ListAllEntriesWithNameAsync(string query);

        /// <summary>
        /// Gets all index entries.
        /// </summary>
        /// <returns>A list of all index entries.</returns>
        Task<List<IndexEntry>> ListAllEntriesAsync();
    }
}