using PassManager.Infrastructure.Encryption;
using PassManager.Infrastructure.Storage;
using PassManager.Shared.Constants;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace PassManager.Core.Password.Read
{
    /// <inheritdoc />
    public class PasswordReader : IPasswordReader
    {
        private readonly IPasswordManagerStorage storage;

        public PasswordReader(IPasswordManagerStorage storage)
        {
            this.storage = storage;
        }

        public async Task<string> GetPasswordAsync(string fileName, string secureString = null)
        {
            using var isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            var entropy = string.IsNullOrEmpty(secureString) ? Configuration.DefaultEntropy : secureString;
            var passwordBytes = await this.storage.ReadBytesAsync(fileName);

            return ProtectedDataUtils.Decrypt(passwordBytes, entropy);
        }
    }
}
