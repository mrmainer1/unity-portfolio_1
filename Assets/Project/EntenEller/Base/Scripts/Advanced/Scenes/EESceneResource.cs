using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Debugs;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.ScriptableObjects;
using Project.EntenEller.Base.Scripts.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.EntenEller.Base.Scripts.Advanced.Scenes
{
    [CreateAssetMenu(fileName = "EEMenuData", menuName = "EntenEller/Scene/EESceneData", order = 1)]
    public class EESceneResource : EEResource
    {
        public string Name;
        public bool IsMain;
        
        public static event Action ScenesStartedChangesEvent;
        public static event Action ScenesFinishedChangesEventRAW, ScenesFinishedChangesEvent;
        
        private static bool isInitialized;
        
        private static readonly List<string> scenesToUnload = new List<string>();
        private static readonly List<string> scenesToLoad = new List<string>();
        private static readonly List<string> scenesActive = new List<string>();
        
        private static string mainScene;
        public static bool IsLoading = false;
        private static int frame;
        
        public void Load(EESceneResource sceneResource)
        {
            if (frame != Time.frameCount) ScenesStartedChangesEvent.Call();
            frame = Time.frameCount;
            
            IsLoading = true;
            if (!isInitialized)
            {
                isInitialized = true;
                
                SceneManager.sceneUnloaded += scene =>
                {
                    scenesToUnload.Remove(scene.name);
                    if (scenesToUnload.Count != 0) return;
                    EEDebug.Log(EEDebugTag.Scene, "Unloaded: " + scene.name);
                    scenesToLoad.ForEach(a => SceneManager.LoadScene(a, LoadSceneMode.Additive));
                };
                
                SceneManager.sceneLoaded += (scene, mode) =>
                {
                    scenesToLoad.RemoveAll(a => a == scene.name);
                    scenesActive.Add(scene.name);
                    EEDebug.Log(EEDebugTag.Scene, "Loaded: " + scene.name);
                    EEDebug.Log(EEDebugTag.Scene, "Left: " + scenesToLoad.Count);
                    if (scene.name == mainScene)
                    {
                        EEDebug.Log(EEDebugTag.Scene, "Set main scene: " + scene.name);
                        SceneManager.SetActiveScene(scene);
                    }
                    if (scenesToLoad.Count != 0) return;
                    EEDebug.Log(EEDebugTag.Scene, "Finished loading scenes");
                    EETime.StartTimer(new EETime.EETimerData
                    {
                        Action = () =>
                        {
                            IsLoading = false;
                            ScenesFinishedChangesEventRAW.Call();
                            EEBehaviourBase.GlobalReinitialization();
                            ScenesFinishedChangesEvent.Call();
                        },
                        IsUnscaled = true,
                        FinalTime = 0.1f
                    });
                };
            }

            if (sceneResource.IsMain)
            {
                mainScene = sceneResource.Name;
                EEDebug.Log(EEDebugTag.Scene, "Trying to set MainScene: " + mainScene);
            }
            
            if (scenesToUnload.Count == 0)
            {
                SceneManager.LoadScene(sceneResource.Name, LoadSceneMode.Additive);
            }
            else
            {
                scenesToLoad.Add(sceneResource.Name);
            }
        }

        public void Unload(EESceneResource sceneResource)
        {
            if (!scenesActive.Contains(sceneResource.Name)) return;
            if (frame != Time.frameCount) ScenesStartedChangesEvent.Call();
            frame = Time.frameCount;
            scenesActive.Remove(sceneResource.Name);
            scenesToUnload.Add(sceneResource.Name);
            SceneManager.UnloadSceneAsync(sceneResource.Name);
        }
        
        public void UnloadAll()
        {
            foreach (var scene in scenesActive)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
            scenesActive.Clear();
        }
    }
}