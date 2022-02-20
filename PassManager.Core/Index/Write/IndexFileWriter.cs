using PassManager.Core.VirtualDirectory;
using PassManager.Infrastructure.Encryption;
using PassManager.Infrastructure.Storage;
using PassManager.Shared.Constants;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PassManager.Core.Index.Write
{
    /// <inheritdoc />
    public class IndexFileWriter : IIndexFileWriter
    {
        private readonly IPasswordManagerStorage storage;

        public IndexFileWriter(IPasswordManagerStorage storage)
        {
            this.storage = storage;
        }

        public async Task InitIndexFile()
        {
            var indexFile = new IndexFile();
            var encyptedBytes = ProtectedDataUtils.EncryptAsBson(indexFile, Configuration.DefaultEntropy);
            await this.storage.WriteBytesAsync(Configuration.IndexFilename, encyptedBytes);
        }

        public async Task AddEntryAsync(IndexEntry entry)
        {
            await this.ExecuteAsync((indexFile) => indexFile.Entries.Add(entry));
        }

        public async Task RemoveEntryByNameAsync(string name)
        {
            await this.ExecuteAsync((indexFile) =>
            {
                var entryIndex = indexFile
                   .Entries
                   .FindIndex(v => string.Equals(v.Name, name, StringComparison.OrdinalIgnoreCase));

                indexFile.Entries.RemoveAt(entryIndex);
            });
        }

        public async Task UpdateEntryByNameAsync(string name, Action<IndexPatchCommand> patch)
        {
            await this.ExecuteAsync((indexFile) =>
            {
                var entry = indexFile
                   .Entries
                   .Single(v => string.Equals(v.Name, name, StringComparison.OrdinalIgnoreCase));

                patch(new IndexPatchCommand(entry));
            });
        }

        private async Task ExecuteAsync(Action<IndexFile> operation)
        {
            var protectedBytes = await this.storage.ReadBytesAsync(Configuration.IndexFilename);
            var indexFile = ProtectedDataUtils.DecryptFromBson<IndexFile>(protectedBytes, Configuration.DefaultEntropy);
            operation(indexFile);
            var encyptedBytes = ProtectedDataUtils.EncryptAsBson(indexFile, Configuration.DefaultEntropy);
            await this.storage.WriteBytesAsync(Configuration.IndexFilename, encyptedBytes);
        }
    }
}
