namespace Project.EntenEller.Base.Scripts.Network.Misc
{
    public static class EEURLUtils
    {
        public static string URLToFileName(string url)
        {
            return url.Replace("://", "_").Replace(".", "_").Replace("/", "_");
        }
    }
}
