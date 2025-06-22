using System.Globalization;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class EEIntUtils
    {
        public static int ParseInt(this string target, int defaultValue = 0)
        {
            var result = int.TryParse(target, NumberStyles.Any, CultureInfo.InvariantCulture, out var r);
            if (!result) r = defaultValue;
            return r;
        }
    }
}
