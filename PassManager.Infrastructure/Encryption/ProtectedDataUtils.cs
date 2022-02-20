using PassManager.Infrastructure.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace PassManager.Infrastructure.Encryption
{
    public static class ProtectedDataUtils
    {
        public static byte[] Encrypt(string text, string entropy)
        {
            var textBytes = Encoding.Unicode.GetBytes(text);
            var entropyBytes = Encoding.Unicode.GetBytes(entropy);

            return ProtectedData.Protect(textBytes, entropyBytes, DataProtectionScope.CurrentUser);
        }

        public static byte[] EncryptAsBson<T>(T value, string entropy)
        {
            var bsonDocumentBytes = BsonSerializer.ToBson<T>(value);
            var entropyBytes = Encoding.Unicode.GetBytes(entropy);

            return ProtectedData.Protect(bsonDocumentBytes, entropyBytes, DataProtectionScope.CurrentUser);
        }

        public static string Decrypt(byte[] encryptedData, string entropy)
        {
            var entropyBytes = Encoding.Unicode.GetBytes(entropy);
            var decryptedBytes = ProtectedData.Unprotect(encryptedData, entropyBytes, DataProtectionScope.CurrentUser);

            return Encoding.Unicode.GetString(decryptedBytes);
        }

        public static T DecryptFromBson<T>(byte[] encryptedData, string entropy)
        {
            var entropyBytes = Encoding.Unicode.GetBytes(entropy);
            var decryptedBytes = ProtectedData.Unprotect(encryptedData, entropyBytes, DataProtectionScope.CurrentUser);

            return BsonSerializer.FromBson<T>(decryptedBytes);
        }
    }
}
