using Project.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.RigidBodies
{
    public class EERigidBody2DApproachPosition : EETransformApproachPosition
    {
        protected override void GlobalMove()
        {
            GetSelf<Rigidbody2D>().MovePosition(Position.Current);
        }

        protected override void LocalMove()
        {
            throw new System.NotImplementedException();
        }
    }
}
