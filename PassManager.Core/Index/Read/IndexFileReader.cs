using PassManager.Core.VirtualDirectory;
using PassManager.Infrastructure.Encryption;
using PassManager.Infrastructure.Storage;
using PassManager.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassManager.Core.Index.Read
{
    /// <inheritdoc />
    public class IndexFileReader : IIndexFileReader
    {
        private readonly IPasswordManagerStorage storage;

        public IndexFileReader(IPasswordManagerStorage storage)
        {
            this.storage = storage;
        }

        public async Task<IndexEntry> GetByNameAsync(string name)
        {
            return await this.Read((indexFile) =>
            {
                return indexFile
                   .Entries
                   .FirstOrDefault(v => v.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            });
        }

        public async Task<List<IndexEntry>> ListAllEntriesWithNameAsync(string name)
        {
            return await this.Read((indexFile) =>
            {
                return indexFile
                   .Entries
                   .Where(v => v.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                   .ToList();
            });
        }

        public async Task<List<IndexEntry>> ListAllEntriesAsync()
        {
            return await this.Read((indexFile) => indexFile.Entries);
        }

        private async Task<T> Read<T>(Func<IndexFile, T> operation)
        {
            var protectedBytes = await this.storage.ReadBytesAsync(Configuration.IndexFilename);
            var indexFile = ProtectedDataUtils.DecryptFromBson<IndexFile>(protectedBytes, Configuration.DefaultEntropy);

            return operation(indexFile);
        }
    }
}
