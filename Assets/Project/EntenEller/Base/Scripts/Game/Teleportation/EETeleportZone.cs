using Project.EntenEller.Base.Scripts.Advanced.Triggers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.GameOnly.Teleportation
{
    public class EETeleportZone : EEBehaviour
    {
        public void Call()
        {
            GetSelf<EETrigger>().RightCollisions.ForEach(a => GetSelf<EETeleportator>().Call(a));
        }
    }
}
