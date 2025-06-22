using System;
using System.Reflection;
using Project.EntenEller.Base.Scripts.Advanced.Actions;
using Project.EntenEller.Base.Scripts.Advanced.Editors;
using Project.EntenEller.Base.Scripts.Advanced.ForEditor;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Notifiers
{
    [ExecuteAfter(typeof(EEAction))]
    public class EENotifierListener : EEBehaviour
    {
        public EEBehaviour Target;
        public string NotifierName;
        public EEAction Action;
        [ReadOnly] [SerializeField] private EENotifier notifier = new EENotifier();
#if DEBUG
        [ReadOnly] public EEEditorTimePoint TimePoint = new();
#endif
        
        protected override void EEAwake()
        {
            base.EEAwake();
            Subscribe();
            notifier.Event += OnEvent;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            notifier.Event -= OnEvent;
        }

        private void Subscribe()
        {
            var behaviours = Target.GetComponents<EEBehaviour>();
            var pathes = NotifierName.Split("/");

            foreach (var behaviour in behaviours)
            {
                var type = behaviour.GetType();
                const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                
                object currentObject = behaviour;
                foreach (var pathComponent in pathes)
                {
                    var memberInfo = type.GetField(pathComponent, bindingFlags);
                    if (memberInfo == null)
                    {
                        currentObject = null;
                        break;
                    }
                    currentObject = memberInfo.GetValue(currentObject);
                    type = memberInfo.FieldType;
                }
                
                if (currentObject != null && currentObject is EENotifier)
                {
                    notifier = (EENotifier)currentObject;
                    return;
                }
            }

            throw new Exception("Target listener is not found: " + NotifierName);
        }

        private void OnEvent()
        {
#if DEBUG
            TimePoint.Refresh();    
#endif
            Action.Call();
        }
    }
}
