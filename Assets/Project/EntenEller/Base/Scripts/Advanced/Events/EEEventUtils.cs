using System;

namespace Project.EntenEller.Base.Scripts.Advanced.Events
{
    public static class EEEventUtils
    {
        public static void Call(this Action action)
        {
            action?.Invoke();
        }

        public static void Call<T1>(this Action<T1> action, T1 a)
        {
            action?.Invoke(a);
        }

        public static void Call<T1, T2>(this Action<T1, T2> action, T1 a, T2 b)
        {
            action?.Invoke(a, b);
        }

        public static void Call<T1, T2, T3>(this Action<T1, T2, T3> action, T1 a, T2 b, T3 c)
        {
            action?.Invoke(a, b, c);
        }
    }
}
