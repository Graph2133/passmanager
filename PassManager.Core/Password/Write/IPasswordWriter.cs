using System.Threading.Tasks;

namespace PassManager.Core.Password.Write
{
    /// <summary>
    /// The password writer.
    /// </summary>
    public interface IPasswordWriter
    {
        /// <summary>
        /// Encrypts password using DPAPI and saves it into isolated storage.
        /// </summary>
        /// <param name="filename">The password filename.</param>
        /// <param name="password">The password.</param>
        /// <param name="secureString">The optional entopy of encryption.</param>
        /// <returns>The <see cref="Task"/> that represents the operation.</returns>
        Task SavePasswordAsync(string filename, string password, string secureString = null);

        /// <summary>
        /// Deletes the password by filename.
        /// </summary>
        /// <param name="filename">The password filename.</param>
        void DeletePasswordAsync(string filename);
    }
}