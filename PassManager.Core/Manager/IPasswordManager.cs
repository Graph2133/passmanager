using PassManager.Core.VirtualDirectory;
using PassManager.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassManager.Core.Repository
{
    /// <summary>
    /// The facade to interract with user-scoped isolated storage directory
    /// and encrypt/decrypt user passwords.
    /// </summary>
    public interface IPasswordManager
    {
        Task<Result> CreatePasswordAsync(string name, string password, string secret = null, List<string> tags = null);

        Task<Result> DeletePasswordByNameAsync(string name);

        Task<Result> DeleteSecret(string name, string secret);

        Task<List<IndexEntry>> GetAllAsync();

        Task<List<IndexEntry>> GetAllByNameAsync(string name);

        Task<DataResult<string>> GetPasswordAsync(string name, string secret = null);

        Task Initialize(bool? deleteExistingContent = null);

        void PurgeAll();

        Task<Result> UpdatePasswordByNameAsync(string name, string updatedName, string password, params string[] tags);

        Task<Result> UpdateSecret(string name, string oldSecret, string newSecret);
    }
}