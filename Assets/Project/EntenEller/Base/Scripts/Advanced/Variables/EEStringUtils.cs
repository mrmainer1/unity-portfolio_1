using System;
using System.Globalization;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class EEStringUtils
    {
        public static string GetBetween(this string source, string start, string end)
        {
            if (!source.Contains(source) || !source.Contains(end)) return string.Empty;
            var i0 = source.IndexOf(start, 0, StringComparison.Ordinal) + start.Length;
            var i1 = source.IndexOf(end, i0, StringComparison.Ordinal);
            return source.Substring(i0, i1 - i0);
        }
        
        public static bool IsEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }
        
        public static bool IsNotEmpty(this string source)
        {
            return !IsEmpty(source);
        }

        public static string FloatToString(this float a, char delimeter = ',')
        {
            return a.ToString(CultureInfo.InvariantCulture).Replace('.', delimeter);
        }
    }
}
