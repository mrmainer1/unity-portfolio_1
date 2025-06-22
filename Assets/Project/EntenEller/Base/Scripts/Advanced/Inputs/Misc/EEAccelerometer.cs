using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Inputs.Misc
{
    public class EEAccelerometer : EEBehaviourUpdate
    {
        public Vector3 Axis { get; private set; }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            Axis = Input.acceleration;
        }
    }
}
