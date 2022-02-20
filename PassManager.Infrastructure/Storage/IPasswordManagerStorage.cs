using System.Threading.Tasks;

namespace PassManager.Infrastructure.Storage
{
    /// <summary>
    /// The password storage. This storage is abstraction around
    /// user-scoped isolated storage directory with name picked up
    /// from configuration.
    /// </summary>
    public interface IPasswordManagerStorage
    {
        /// <summary>
        /// Initializes directory which will be used to store passwords.
        /// </summary>
        void InitializeStorage();

        /// <summary>
        /// Deletes file from storage.
        /// </summary>
        /// <param name="filename">The filename</param>
        void DeleteFileAsync(string filename);

        /// <summary>
        /// Removes app directory from isolated storage.
        /// </summary>
        void PurgeStorage();

        /// <summary>
        /// Reads file from storage.
        /// </summary>
        /// <param name="filename">The </param>
        /// <returns></returns>
        Task<byte[]> ReadBytesAsync(string filename);

        /// <summary>
        /// Writes file into storage.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="bytes">The bytes to store.</param>
        /// <returns></returns>
        Task WriteBytesAsync(string filename, byte[] bytes);
    }
}