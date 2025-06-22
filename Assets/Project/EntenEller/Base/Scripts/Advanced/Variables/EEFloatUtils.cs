using System;
using System.Collections.Generic;
using System.Globalization;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class EEFloatUtils
    {
        public static bool IsAlmostZero(this float a, float measurementAccuracy = EEConstants.MeasurementAccuracyHigh)
        {
            return a <= measurementAccuracy && a >= -measurementAccuracy;
        }

        public static bool IsAlmostEqual(this float a, float b, float measurementAccuracy = EEConstants.MeasurementAccuracyHigh)
        {
            return (a - b) <= measurementAccuracy && (a - b) >= -measurementAccuracy;
        }

        public static bool IsBetween(this float a, float b, float c)
        {
            if (b > c) return a >= c && a <= b;
            return a >= b && a <= c;
        }
        
        public static void Swap(ref float a, ref float b)
        {
            (a, b) = (b, a);
        }

        public static float ParseFloat(this string target, float defaultValue = 0f)
        {
            var result = float.TryParse(target, NumberStyles.Any, CultureInfo.InvariantCulture, out var r);
            if (!result) r = defaultValue;
            return r;
        }
        
        public static string StringifyWithDots(this float target, int decimalAmount = 2)
        {
            return target.ToString($"F{decimalAmount}", CultureInfo.InvariantCulture);
        }

        public static string Stringify(this IList<float> list)
        {
            var r = "";
            for (var i = 0; i < list.Count; i++)
            {
                r += list[i].StringifyWithDots() + ";";
            }

            return r;
        }
        
        public static IList<float> ParseListFloat(this string data)
        {
            var result = new List<float>();
            var elements = data.Split(';');

            foreach (var element in elements)
            {
                result.Add(element.ParseFloat(4));
            }

            return result;
        }

    }
}
