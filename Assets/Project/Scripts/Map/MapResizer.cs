using System;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Timers;
using Sirenix.OdinInspector;

namespace Project.Scripts.Map
{
    public class MapResizer : EEBehaviour
    {
        public int Width, Height;
        private int newWidth, newHeight;
        public EENotifier ChangeSizeStartNotifier, ChangeSizeFinishNotifier;
        
        [Button]
        public void SetSize(int w, int h)
        {
            if (w < 0 || h < 0) throw new Exception("Width or height is wrong!");
            Width = w;
            Height = h;
            ChangeSizeStartNotifier.Notify();
            EETime.StartTimer(new EETime.EETimerData
            {
                Action = () =>
                {
                    ChangeSizeFinishNotifier.Notify();
                },
                FinalTime = 0.1f
            });
        }

        public void SetSize()
        {
            SetSize(newWidth,newHeight);
        }

        public void RememberSize(int w, int h)
        {
            newWidth = w;
            newHeight = h;
        }
        
        public void Set30Size() 
        {
            SetSize(10, 10);
        }

        public void SetDefaultSize() => Set30Size();
    }
}
