using System.IO;
using System.IO.Compression;

namespace Project.EntenEller.Base.Scripts.Compressions
{
    public static class EECompressionUtils
    {
        public static byte[] Compress(byte[] data, CompressionLevel level = CompressionLevel.Optimal)
        {
            var output = new MemoryStream();
            using (var stream = new DeflateStream(output, level))
            {
                stream.Write(data, 0, data.Length);
            }
            return output.ToArray();
        }

        public static byte[] Decompress(byte[] data)
        {
            var input = new MemoryStream(data);
            var output = new MemoryStream();
            using (var stream = new DeflateStream(input, CompressionMode.Decompress))
            {
                stream.CopyTo(output);
            }
            return output.ToArray();
        }
    }
}
