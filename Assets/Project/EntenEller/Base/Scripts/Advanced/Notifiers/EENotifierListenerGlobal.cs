using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Actions;
using Project.EntenEller.Base.Scripts.Advanced.Editors;
using Project.EntenEller.Base.Scripts.Advanced.ForEditor;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Scenes;
using Project.EntenEller.Base.Scripts.Advanced.Spawns;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Notifiers
{
    public class EENotifierListenerGlobal : EEVariableFinder
    {
        [ReadOnly] [SerializeField] private List<EENotifier> notifiers = new List<EENotifier>();
        public int Amount;
        public EEAction Action;
#if DEBUG
        [ReadOnly] public EEEditorTimePoint TimePointCall = new();
#endif
        
        protected override void EEAwake()
        {
            base.EEAwake();
            EESpawnUtils.SpawnDoneEvent += Search;
            EESceneResource.ScenesFinishedChangesEvent += Search;
            IsActiveUpdating = false;
            Search();
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            EESpawnUtils.SpawnDoneEvent -= Search;
            EESceneResource.ScenesFinishedChangesEvent -= Search;
            foreach (var notifier in notifiers)
            {
                notifier.Event -= OnEvent;
                Amount--;
            }
        }

        public void Search()
        {
            foreach (var notifier in notifiers)
            {
                notifier.Event -= OnEvent;
                Amount--;
            }
            notifiers.Clear();
            foreach (var data in VariablesInfo.SelectMany(a => a.Variables))
            {
                notifiers.Add(data.Value as EENotifier);
            }
            foreach (var notifier in notifiers)
            {
                notifier.Event += OnEvent;
                Amount++;
            }
        }

        private void OnEvent()
        {
#if DEBUG
            TimePointCall.Refresh();    
#endif
            Action.Call();
        }
    }
}
