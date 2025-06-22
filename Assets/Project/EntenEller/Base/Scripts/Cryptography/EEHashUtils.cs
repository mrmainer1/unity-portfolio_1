using System.Security.Cryptography;
using System.Text;

namespace Project.EntenEller.Base.Scripts.Cryptography
{
    public static class EEHashUtils
    {
        public static string SHA1(byte[] data)
        {
            using var sha1 = new SHA1Managed();
            var hash = sha1.ComputeHash(data);
            var sb = new StringBuilder(hash.Length * 2);
            foreach (var b in hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        
        public static int StringToIntHash(this string source)
        {
            const int fnvPrime = 16777619;
            var hash = 216613621;

            foreach (var c in source)
            {
                hash ^= c;
                hash *= fnvPrime;
            }
            
            return hash;
        }
    }
}
