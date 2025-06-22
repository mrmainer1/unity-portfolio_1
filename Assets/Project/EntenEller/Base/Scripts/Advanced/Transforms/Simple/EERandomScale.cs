using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EERandomScale : EEBehaviour
    {
        [SerializeField] private float min, max;
        
        public void Call()
        {
            var r = Random.Range(min, max);
            GetSelf<Transform>().localScale = new Vector3(r, r, r);
        }
    }
}
