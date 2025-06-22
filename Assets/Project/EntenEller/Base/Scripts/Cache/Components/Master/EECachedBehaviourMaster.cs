using Project.EntenEller.Base.Scripts.Advanced.Behaviours;

namespace Project.EntenEller.Base.Scripts.Cache.Components.Master
{
    public abstract class EECachedBehaviourMaster : EEBehaviourBase
    {
        protected T CreateCachedComponent<T>() where T : EECachedComponent
        {
            return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
        }
    }
}