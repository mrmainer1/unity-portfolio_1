using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Triggers
{
    public class EETriggerColliderListener3D : EETriggerColliderListener
    {
        private void OnTriggerEnter(Collider they)
        {
            Enter(they.GetComponent<EETriggerColliderListener>());
        }
        
        private void OnTriggerExit(Collider they)
        {
            Exit(they.GetComponent<EETriggerColliderListener>());
        }
    }
}
