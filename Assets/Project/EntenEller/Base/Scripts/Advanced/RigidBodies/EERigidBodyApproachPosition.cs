using Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.RigidBodies
{
    public class EERigidBodyApproachPosition : EETransformApproachPosition
    {
        protected override void GlobalMove()
        {
            GetSelf<Rigidbody>().position = Position.Current;
        }

        protected override void LocalMove()
        {
            throw new System.NotImplementedException();
        }
    }
}
