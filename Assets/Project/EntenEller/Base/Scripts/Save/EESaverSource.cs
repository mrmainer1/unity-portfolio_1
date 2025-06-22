using System;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Save
{
    [ExecuteBefore(typeof(EESaver))]
    public abstract class EESaverSource : EEBehaviour
    {
        public abstract void Save(EESaver saver);
        public abstract void Load(EESaver saver, Action<string> actionOnLoad);

        protected static void SaveFinish()
        {
            
        }
        
        protected static void LoadFinish(Action<string> actionOnLoad, string value)
        {
            actionOnLoad.Invoke(value);
        }
    }
}