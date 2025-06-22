using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Advanced.GameObjects
{
    public class EENonDestroyable : EEBehaviour
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            DontDestroyOnLoad(gameObject);
        }
    }
}
