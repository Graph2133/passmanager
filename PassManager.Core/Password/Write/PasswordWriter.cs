using PassManager.Infrastructure.Encryption;
using PassManager.Infrastructure.Storage;
using PassManager.Shared.Constants;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace PassManager.Core.Password.Write
{
    /// <inheritdoc />
    public class PasswordWriter : IPasswordWriter
    {
        private readonly IPasswordManagerStorage storage;

        public PasswordWriter(IPasswordManagerStorage storage)
        {
            this.storage = storage;
        }

        public async Task SavePasswordAsync(string fileName, string password, string secureString = null)
        {
            using var isoStore = IsolatedStorageFile.GetUserStoreForApplication();

            var entropy = string.IsNullOrEmpty(secureString) ? Configuration.DefaultEntropy : secureString;
            var encryptedBytes = ProtectedDataUtils.Encrypt(password, entropy);

            await this.storage.WriteBytesAsync(fileName, encryptedBytes);
        }

        public void DeletePasswordAsync(string fileName) => this.storage.DeleteFileAsync(fileName);
    }
}
