using Project.EntenEller.Base.Scripts.Advanced.Randoms;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EERandomRotation : EEBehaviour
    {
        [SerializeField] private Vector3 min, max;
        
        public void Call()
        {
            GetSelf<Transform>().localEulerAngles = new Vector3
            (
                EERandomUtils.RandomFloat(min.x, max.x),
                EERandomUtils.RandomFloat(min.y, max.y),
                EERandomUtils.RandomFloat(min.z, max.z)
            );
        }
    }
}
