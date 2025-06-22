using System;
using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Randoms;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class EEArrayUtils
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var count = list.Count;  
            while (count > 1) 
            {  
                count--;
                var k = EERandomUtils.RandomPositiveInt(count);
                (list[k], list[count]) = (list[count], list[k]);
            }  
        }
        
        public static T GetRandom<T>(this IList<T> list)
        {
            return list[EERandomUtils.RandomPositiveInt(list.Count)];
        }

        public static T GetLooped<T>(this IList<T> list, ref int i)
        {
            i %= list.Count;
            if (i < 0) i = list.Count + i;
            return list[i];
        }
        
        public static void Switch<T>(this IList<T> list, int a, int b)
        {
            (list[a], list[b]) = (list[b], list[a]);
        }

        public static bool IsTheSameWithOrder<T>(this IList<T> list1, IList<T> list2)
        {
            if (list1.Count != list2.Count) return false;
            for (var i = 0; i < list1.Count; i++)
            {
                if (!list1[i].Equals(list2[i])) return false;
            }
            return true;
        }
        
        public static bool IsTheSameAnyOrder<T>(this IList<T> list1, IList<T> list2)
        {
            if (list1.Count != list2.Count) return false;
            while (list1.Count != 0)
            {
                var first = list1.First();
                if (!list2.Contains(first)) return false;
                list2.Remove(first);
            }
            return true;
        }
    }
}
