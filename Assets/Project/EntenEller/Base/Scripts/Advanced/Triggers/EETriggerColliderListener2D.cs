using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Triggers
{
    public class EETriggerColliderListener2D : EETriggerColliderListener
    {
        private void OnTriggerEnter2D(Collider2D they)
        {
            Enter(they.GetComponent<EETriggerColliderListener>());
        }

        private void OnTriggerExit2D(Collider2D they)
        {
            Exit(they.GetComponent<EETriggerColliderListener>());
        }
    }
}
