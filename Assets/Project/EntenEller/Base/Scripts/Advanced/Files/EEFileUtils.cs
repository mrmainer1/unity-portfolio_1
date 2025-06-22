using System.IO;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Files
{
    public static class EEFileUtils
    {
        public static void Save(byte[] bytes, string uniqName)
        {
            File.WriteAllBytes(Application.dataPath + "/" + uniqName, bytes);
        }

        public static byte[] Load(string uniqName)
        {
            return File.ReadAllBytes(Application.dataPath + "/" + uniqName);
        }

        public static bool IsExist(string uniqName)
        {
            return File.Exists(Application.dataPath + "/" + uniqName);
        }
        
        public static string AbsolutePathToAssetPath(string absolutePath)
        {
            return "Assets" + absolutePath.Substring(Application.dataPath.Length);
        }
    }
}
