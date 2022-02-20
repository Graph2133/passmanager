using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.IO;

namespace PassManager.Infrastructure.Serialization
{
    public static class BsonSerializer
    {
        public static byte[] ToBson<T>(T value)
        {
            using var ms = new MemoryStream();
            using var dataWriter = new BsonDataWriter(ms);
            var serializer = new JsonSerializer();
            serializer.Serialize(dataWriter, value);

            return ms.ToArray();
        }

        public static T FromBson<T>(byte[] data)
        {
            using MemoryStream ms = new MemoryStream(data);
            using BsonDataReader reader = new BsonDataReader(ms);
            JsonSerializer serializer = new JsonSerializer();

            return serializer.Deserialize<T>(reader);
        }
    }
}
