using System.Numerics;

namespace Project.EntenEller.Base.Scripts.Advanced.Maths
{
    public static class EEBigIntUtils
    {
        public static BigInteger Lerp(BigInteger start, BigInteger end, double t)
        {
            const int factor = 100;
            var d = (int) (t * factor);
            var result = start + (end - start) * d / factor;
            return result;
        }
    }
}