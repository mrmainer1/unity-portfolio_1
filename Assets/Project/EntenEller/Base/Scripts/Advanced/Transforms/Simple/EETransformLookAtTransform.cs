using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Tags;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class EETransformLookAtTransform : EEBehaviourUpdate
    {
        [SerializeField] private EEGameObjectFinder target;
        
        protected override void EEUpdate()
        {
            base.EEUpdate();
            var ts = target.GetSingle().transform;
            transform.rotation = Quaternion.Euler(ts.eulerAngles.x, ts.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
