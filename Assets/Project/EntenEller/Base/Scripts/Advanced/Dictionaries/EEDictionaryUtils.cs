using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Randoms;

namespace Project.EntenEller.Base.Scripts.Advanced.Dictionaries
{
    public static class EEDictionaryUtils
    {
        public static KeyValuePair<T1, T2> GetRandom<T1, T2>(this Dictionary <T1, T2> dictionary)
        {
            return dictionary.ElementAt(EERandomUtils.RandomPositiveInt(dictionary.Count));
        }
    }
}