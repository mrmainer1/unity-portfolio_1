using System.Collections.Generic;

namespace Project.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class EEListUtils
    {
        public static bool IsFullyEqual<T>(this List<T> list1, List<T> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }
            for (var i = 0; i < list1.Count; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(list1[i], list2[i]))
                {
                    return false;
                }
            }
            return true;
        }
        
        private static bool IsFullyEqual<T>(T[] array1, T[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (var i = 0; i < array1.Length; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(array1[i], array2[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
