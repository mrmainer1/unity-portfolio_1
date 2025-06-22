using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Maths;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class EEVectorUtils
    {
        public static bool IsAlmostEqual(this Vector3 a, Vector3 b, float measurementAccuracy = EEConstants.MeasurementAccuracyHigh)
        {
            return (a - b).sqrMagnitude.IsAlmostZero(measurementAccuracy);
        }
        
        public static bool IsAlmostEqual(this Vector2 a, Vector2 b, float measurementAccuracy = EEConstants.MeasurementAccuracyHigh)
        {
            return (a - b).sqrMagnitude.IsAlmostZero(measurementAccuracy);
        }

        public static Vector3 ListToV3(List<float> list)
        {
            var x = list[0];
            var y = list[1];
            var z = list[2];
            return new Vector3(x, y, z);
        }
        
        public static List<float> V3ToList(Vector3 v3)
        {
            return new List<float>{v3.x, v3.y, v3.z};
        }
        
        public static Vector3 RandomV3(Vector3 a, Vector3 b)
        {
            return new Vector3(Random.Range(a.x, b.x), Random.Range(a.y, b.y), Random.Range(a.z, b.z));
        }
        
        public static Vector3 Round(this Vector3 a, int decimals = 0)
        {
            a.x = a.x.RoundFloat(decimals);
            a.y = a.y.RoundFloat(decimals);
            a.z = a.z.RoundFloat(decimals);
            return a;
        }

        public static Vector3 StringToV3(string a, string b, string c)
        {
            var v3 = Vector3.zero;
            float.TryParse(a, out v3.x);
            float.TryParse(b, out v3.y);
            float.TryParse(c, out v3.z);
            return v3;
        }
    }
}