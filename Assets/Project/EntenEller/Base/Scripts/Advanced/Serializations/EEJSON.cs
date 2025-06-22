using CatJson;

namespace Project.EntenEller.Base.Scripts.Advanced.Serializations
{
    public static class EEJSON
    {
        public static string Serialize<T>(T data)
        {
            return JsonParser.Default.ToJson(data);
        }
 
        public static T Deserialize<T>(string data)
        {
            return JsonParser.Default.ParseJson<T>(data);
        }
    }
}