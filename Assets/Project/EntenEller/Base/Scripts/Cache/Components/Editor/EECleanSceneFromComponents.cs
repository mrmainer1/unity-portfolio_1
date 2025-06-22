using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Editor
{
    public static class EECleanSceneFromComponents 
    {
        [MenuItem("Window/EntenEller/Clean Scene From Components")]
        private static void Clean()
        {
            Object.FindObjectsByType<EECachedComponent>(FindObjectsInactive.Include, FindObjectsSortMode.None).ForEach(a => Object.DestroyImmediate(a));
        }
    }
}