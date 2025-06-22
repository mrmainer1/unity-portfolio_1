using Project.EntenEller.Base.Scripts.Advanced.Editors;
using Project.EntenEller.Base.Scripts.Advanced.ForEditor;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Actions
{
    [ExecutionOrder(9999)]
    public class EEActionAutoCaller : EEBehaviour
    {
        [SerializeField] private EEAction action;
        public bool CallOnAwake, CallOnEnable;
#if DEBUG
        [ReadOnly] public EEEditorTimePoint TimePointAwake = new();
        [ReadOnly] public EEEditorTimePoint TimePointEnable = new();
#endif
        
        protected override void EEAwake()
        {
            base.EEAwake();
            if (!CallOnAwake) return;
#if DEBUG
            TimePointAwake.Refresh();
#endif
            action.Call();
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            if (!CallOnEnable) return;
#if DEBUG
            TimePointEnable.Refresh();
#endif
            action.Call();
        }
    }
}
