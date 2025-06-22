using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.GameObjects;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Platform;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Advanced.Ad
{
    public abstract class EEAdNormal : EEPlatformKeyBehaviour
    {
        protected static string ID;
        
        private static bool isLoaded;
        private static bool isReadyToShow;
        
        private static EEBehaviour current;
        
        public EENotifier StartLoadEventEE, FinishLoadEventEE, ShowEventEE;
        public EENotifier SkipEventEE, CompleteEventEE, ErrorEventEE;

        protected bool StartLoad()
        {
            if (isLoaded) return false;
            isLoaded = true;
            StartLoadEventEE.Notify();
            return true;
        }
        
        protected void FinishLoad(string id)
        {
            ID = id;
            isLoaded = true;
            FinishLoadEventEE.Notify();
        }
        
        public void StartShow()
        {
            current = this;
            isReadyToShow = true;
            ShowEventEE.Notify();
        }
        
        protected void Error()
        {
            ErrorEventEE.Notify();
            Restart();
        }

        protected void Skip()
        {
            if (current != this) return;
            SkipEventEE.Notify();
            Restart();
        }
        
        protected void Complete()
        {
            if (current != this) return;
            CompleteEventEE.Notify();
            Restart();
        }
        
        private void Restart()
        {
            ID = string.Empty;
            isReadyToShow = false;
            isLoaded = false;
            Load();
        }
        
        protected override void EEUpdate()
        {
            base.EEUpdate();
            if (!isReadyToShow) return;
            if (!isLoaded) return;
            Show();
        }

        protected abstract void Load();
        
        protected abstract void Show();
    }
}
