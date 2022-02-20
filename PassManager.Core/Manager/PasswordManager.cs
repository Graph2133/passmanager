using PassManager.Core.Index.Read;
using PassManager.Core.Index.Write;
using PassManager.Core.Password.Read;
using PassManager.Core.Password.Write;
using PassManager.Core.VirtualDirectory;
using PassManager.Infrastructure.Storage;
using PassManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassManager.Core.Repository
{
    /// <inheritdoc />
    public class PasswordManager : IPasswordManager
    {
        private readonly IPasswordManagerStorage storage;
        private readonly IPasswordReader passwordReader;
        private readonly IPasswordWriter passwordWriter;
        private readonly IIndexFileReader indexFileReader;
        private readonly IIndexFileWriter indexFileWriter;

        public PasswordManager(
            IPasswordManagerStorage storage,
            IPasswordReader passwordReader,
            IPasswordWriter paswordWriter,
            IIndexFileReader indexFileReader,
            IIndexFileWriter indexFileWriter)
        {
            this.storage = storage;
            this.passwordReader = passwordReader;
            this.passwordWriter = paswordWriter;
            this.indexFileReader = indexFileReader;
            this.indexFileWriter = indexFileWriter;
        }

        public async Task Initialize(bool? deleteExistingContent = null)
        {
            if (deleteExistingContent == true)
            {
                this.storage.PurgeStorage();
            }

            this.storage.InitializeStorage();
            await this.indexFileWriter.InitIndexFile();
        }

        public async Task<Result> CreatePasswordAsync(string name, string password, string secret = null, List<string> tags = null)
        {
            var existingEntry = await this.indexFileReader.GetByNameAsync(name);
            if (existingEntry != null)
            {
                return Result.Failed("The password entry already exists.");
            }

            var passFilename = Guid.NewGuid().ToString();
            await this.passwordWriter.SavePasswordAsync(passFilename, password, secret);

            var indexEntry = new IndexEntry
            {
                Filename = passFilename,
                Name = name,
                HasCustomSecret = !string.IsNullOrEmpty(secret),
                Tags = tags ?? new List<string>(0)
            };
            await this.indexFileWriter.AddEntryAsync(indexEntry);

            return Result.Ok();
        }

        public async Task<Result> UpdatePasswordByNameAsync(string name, string updatedName = null, string password = null, params string[] tags)
        {
            var entry = await this.indexFileReader.GetByNameAsync(name);
            if (entry == null)
            {
                return Result.Failed("The password entry does not exist.");
            }

            var entryByUpdateName = await this.indexFileReader.GetByNameAsync(updatedName);
            if (entryByUpdateName != null && !name.Equals(updatedName))
            {
                return Result.Failed("Entry with the same name already exists.");
            }

            await this.indexFileWriter.UpdateEntryByNameAsync(name, c =>
            {
                if (!string.IsNullOrEmpty(updatedName))
                {
                    c.SetName(updatedName);
                }

                if (tags.Any())
                {
                    c.SetTags(tags);
                }
            });


            if (!string.IsNullOrEmpty(password))
            {
                await this.passwordWriter.SavePasswordAsync(entry.Filename, password);
            }

            return Result.Ok();
        }

        public async Task<Result> UpdateSecret(string name, string oldSecret, string newSecret)
        {
            if (string.IsNullOrEmpty(newSecret) || string.IsNullOrEmpty(newSecret))
            {
                return Result.Failed("The provided new or old secret are invalid.");
            }

            var entry = await this.indexFileReader.GetByNameAsync(name);
            if (entry == null)
            {
                return Result.Failed("The password entry does not exist.");
            }

            var password = await this.passwordReader.GetPasswordAsync(entry.Filename, oldSecret);
            await this.passwordWriter.SavePasswordAsync(entry.Filename, password, newSecret);

            return Result.Ok();
        }


        public async Task<Result> DeletePasswordByNameAsync(string name)
        {
            var entry = await this.indexFileReader.GetByNameAsync(name);
            if (entry == null)
            {
                return Result.Failed("The password entry does not exist.");
            }

            await this.indexFileWriter.RemoveEntryByNameAsync(name);
            this.passwordWriter.DeletePasswordAsync(entry.Filename);

            return Result.Ok();
        }

        public async Task<Result> DeleteSecret(string name, string secret)
        {
            var entry = await this.indexFileReader.GetByNameAsync(name);
            if (entry == null)
            {
                return Result.Failed("The password entry does not exist.");
            }

            var getPasswordResult = await this.GetPasswordAsync(name, secret);
            if (!getPasswordResult.Success)
            {
                return Result.Failed(getPasswordResult.ErrorMessage);
            }

            await this.passwordWriter.SavePasswordAsync(entry.Filename, getPasswordResult.ExtensionData);
            await this.indexFileWriter.UpdateEntryByNameAsync(name, c =>
            {
                c.SetHasCustomSecret(false);
            });

            return Result.Ok();
        }

        public async Task<List<IndexEntry>> GetAllByNameAsync(string name)
        {
            return await this.indexFileReader.ListAllEntriesWithNameAsync(name);
        }

        public async Task<List<IndexEntry>> GetAllAsync()
        {
            return await this.indexFileReader.ListAllEntriesAsync();
        }

        public async Task<DataResult<string>> GetPasswordAsync(string name, string secret = null)
        {
            var entry = await this.indexFileReader.GetByNameAsync(name);
            if (entry == null)
            {
                return Result.Of<string>().WithErrorMessage("The password entry does not exist.");
            }

            try
            {
                var password = await this.passwordReader.GetPasswordAsync(entry.Filename, secret);
                return Result.Of<string>().WithExtensionData(password);
            }
            catch
            {
                return Result.Of<string>().WithErrorMessage("The password entry can not be decrypted.");
            }
        }

        public void PurgeAll() => this.storage.PurgeStorage();
    }
}
