using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Raycasters
{
    public class EERaycaster3D : EERaycaster
    {
        public override int CastRay()
        {
            return UnityEngine.Physics.RaycastNonAlloc(new Ray(GetSelf<Transform>().position, GetSelf<Transform>().forward), Hits, Distance, LayerMask);
        }
    }
}
