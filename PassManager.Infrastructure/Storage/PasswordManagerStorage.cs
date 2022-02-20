using PassManager.Shared.Constants;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace PassManager.Infrastructure.Storage
{
    public class PasswordManagerStorage : IPasswordManagerStorage
    {
        public void InitializeStorage()
        {
            using var isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            isoStore.CreateDirectory(Configuration.AppName);
        }

        public async Task WriteBytesAsync(string fileName, byte[] bytes)
        {
            var relativePath = $"{Configuration.AppName}/{fileName}";
            using var isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            using var file = isoStore.OpenFile(relativePath, FileMode.OpenOrCreate);

            await file.WriteAsync(bytes);
        }

        public async Task<byte[]> ReadBytesAsync(string fileName)
        {
            using var isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            using var fileStream = isoStore.OpenFile($"{Configuration.AppName}/{fileName}", FileMode.Open);
            using var memoryStream = new MemoryStream();

            await fileStream.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }

        public void DeleteFileAsync(string fileName)
        {
            using var isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            isoStore.DeleteFile($"{Configuration.AppName}/{fileName}");
        }

        public void PurgeStorage()
        {
            using var isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            isoStore.Remove();
        }
    }
}
