using PassManager.Core.VirtualDirectory;
using System;
using System.Threading.Tasks;

namespace PassManager.Core.Index.Write
{
    /// <summary>
    /// The index file writer. All index entries are serialized
    /// in BSON format and encrypted using DPAPI.
    /// </summary>
    public interface IIndexFileWriter
    {
        /// <summary>
        /// Initializes index file in user isolated storage.
        /// </summary>
        /// <returns>The <see cref="Task"/> that represents the operation.</returns>
        Task InitIndexFile();

        /// <summary>
        /// Adds the index(password) entry.
        /// </summary>
        /// <param name="entry">The index entry.</param>
        /// <returns>The <see cref="Task"/> that represents the operation.</returns>
        Task AddEntryAsync(IndexEntry entry);

        /// <summary>
        /// Removes the index entry by name.
        /// </summary>
        /// <param name="name">The name of the index entry.</param>
        /// <returns>The <see cref="Task"/> that represents the operation.</returns>
        Task RemoveEntryByNameAsync(string name);

        /// <summary>
        /// Updates the index entry by name.
        /// </summary>
        /// <param name="name">The name of the index entry.</param>
        /// <param name="patch">The <see cref="Action"/> to patch index entry.</param>
        /// <returns>The <see cref="Task"/> that represents the operation.</returns>
        Task UpdateEntryByNameAsync(string name, Action<IndexPatchCommand> patch);
    }
}