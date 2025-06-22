namespace Project.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class EEBoolUtils
    {
        public static bool ParseBool (this string str)
        {
            return str.ToLower() == "true" || str == "1";
        }
        
        public static string Stringify (this bool b)
        {
            return b ? "1" : "0";
        }
    }
}