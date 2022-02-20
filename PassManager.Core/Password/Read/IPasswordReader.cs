using System.Threading.Tasks;

namespace PassManager.Core.Password.Read
{
    /// <summary>
    /// The password reader.
    /// </summary>
    public interface IPasswordReader
    {
        /// <summary>
        /// Reads the password by filename and decrypts it.
        /// </summary>
        /// <param name="filename">The password filename.</param>
        /// <param name="secureString">A secret to decrypt password.</param>
        /// <returns>The decrypted password.</returns>
        Task<string> GetPasswordAsync(string filename, string secureString = null);
    }
}