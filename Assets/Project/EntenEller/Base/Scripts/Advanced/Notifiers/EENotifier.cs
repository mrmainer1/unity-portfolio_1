using System;
using Project.EntenEller.Base.Scripts.Advanced.Editors;
using Sirenix.OdinInspector;
using Project.EntenEller.Base.Scripts.Advanced.Events;

namespace Project.EntenEller.Base.Scripts.Advanced.Notifiers
{
    [Serializable]
    public class EENotifier
    {
        public event Action Event;
#if DEBUG
        [ReadOnly] public EEEditorTimePoint TimePointNotify = new();
#endif
        public void Notify()
        {
#if DEBUG
            TimePointNotify.Refresh();    
#endif
            Event.Call();
        }
    }
}
