using System;
using Random = UnityEngine.Random;

namespace Project.EntenEller.Base.Scripts.Advanced.Randoms
{
    public static class EERandomUtils
    {
        public static int RandomInt(int min, int max)
        {
            return Random.Range(min, max);
        }
        
        public static int RandomPositiveInt(int max)
        {
            return Random.Range(0, max);
        }
        
        public static float RandomFloat(float min, float max)
        {
            return Random.Range(min, max);
        }
        
        public static bool RandomBool()
        {
            return Random.Range(0f, 1f) >= 0.5f;
        }
        
        public static string RandomLetterString(int size)
        {
            return GenerateString(size, "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }
        
        public static string RandomAlphaNumericString(int size)
        {
            return GenerateString(size, "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
        }
        
        public static string RandomSymbolString(int size)
        {
            return GenerateString(size, "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*();№:?[]<>{}.,-+");
        }

        private static string GenerateString(int size, string alphabet)
        {
            var chars = new char[size];
            for (var i=0; i < size; i++)
            {
                chars[i] = alphabet[RandomPositiveInt(alphabet.Length)];
            }
            return new string(chars);
        }

        public static string RandomTimeBasedString(int size = 8)
        {
            return RandomAlphaNumericString(size) + "[" + DateTime.Now.ToFileTime() + "]";
        }
    }
}