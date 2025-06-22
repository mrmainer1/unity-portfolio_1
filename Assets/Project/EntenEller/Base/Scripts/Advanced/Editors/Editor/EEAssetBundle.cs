using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.ForEditor.Editor
{
    public class EEAssetBundle : OdinEditorWindow
    {
        [MenuItem("Window/EntenEller/Asset Bundle")]
        private static void OpenWindow()
        {
            GetWindow<EEAssetBundle>().Show();
        }
        
        [Button(ButtonSizes.Large)]
        public void GenerateWindows64()
        {
            BuildPipeline.BuildAssetBundles(GetPath(RuntimePlatform.WindowsPlayer), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
            GUIUtility.ExitGUI();
        }
        
        [Button(ButtonSizes.Large)]
        public void GenerateIOS()
        {
            BuildPipeline.BuildAssetBundles(GetPath(RuntimePlatform.IPhonePlayer), BuildAssetBundleOptions.None, BuildTarget.iOS);
            GUIUtility.ExitGUI();
        }
        
        [Button(ButtonSizes.Large)]
        public void GenerateAndroid()
        {
            BuildPipeline.BuildAssetBundles(GetPath(RuntimePlatform.Android), BuildAssetBundleOptions.None, BuildTarget.Android);
            GUIUtility.ExitGUI();
        }
        
        private string GetPath(RuntimePlatform platform)
        {
            var key = ToString() + platform;
            var path = PlayerPrefs.HasKey(key)
                ? PlayerPrefs.GetString(key)
                : Application.dataPath;
            path = EditorUtility.OpenFolderPanel("Choose Asset Bundles Folder", path, string.Empty);
            var isError = path.Length == 0;
            if (isError)
            {
                EditorUtility.DisplayDialog("Error!", "Invalid save path!", "Ok");
                GUIUtility.ExitGUI();
                throw new Exception("Invalid save path!");
            }
            PlayerPrefs.SetString(key, path);
            path += "/" + platform;
            System.IO.Directory.CreateDirectory(path);
            return path;
        }
    }
}
