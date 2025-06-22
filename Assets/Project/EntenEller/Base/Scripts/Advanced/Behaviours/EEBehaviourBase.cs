using System.ComponentModel;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Components;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Scenes;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Behaviours
{
    public abstract class EEBehaviourBase : EEBehaviourValues
    {
        private static bool isQuit;
        [EditorBrowsable(EditorBrowsableState.Never)] [HideInInspector] public bool IsAwaken;
        private bool needToAwake, needToEnable;
        private int awakeIndex;
        private static int awakeIndexGlobal;

        [Browsable(false)] [EditorBrowsable(EditorBrowsableState.Never)]
        private void Awake()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif
            awakeIndexGlobal++;
            awakeIndex = awakeIndexGlobal;
            if (!EESceneResource.IsLoading)
            {
                IsAwaken = true;
                EEAwake();
            }
            else needToAwake = true;
        }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void OnEnable()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif
            if (!EESceneResource.IsLoading) EEEnable();
            else needToEnable = true;
        }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected virtual void OnDisable()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif
            if (!isQuit) EEDisable();
        }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void OnDestroy()
        {
            if (isQuit) return;
            var obj = GetComponent<EEGameObject>();
            if (obj.IsNotNull())
            {
                EEGameObjectUtils.Remove(obj);
            }
            EEDestroy();
        }
        
        private void OnApplicationQuit()
        {
            isQuit = true;
        }

        public void SceneAwake()
        {
            if (!needToAwake) return;
            needToAwake = false;
            IsAwaken = true;
            EEAwake();
        }

        public void SceneEnable()
        {
            if (!needToEnable) return;
            needToEnable = false;
            EEEnable();
        }

        public static void GlobalReinitialization()
        {
            var list = FindObjectsByType<EEBehaviourBase>(FindObjectsInactive.Include, FindObjectsSortMode.None).OrderBy(a => a.awakeIndex).ToList();
            foreach (var init in list)
            {
                init.SceneAwake();
                init.SceneEnable();
            }
            awakeIndexGlobal = 0;
        }
        
        protected virtual void EEAwake () {}
        protected virtual void EEDestroy () {}
        protected virtual void EEEnable () {}
        protected virtual void EEDisable () {}
    }
}
