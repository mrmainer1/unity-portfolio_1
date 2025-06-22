using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts._2DOnly
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public abstract class EEZOrderBase : EEBehaviourUpdate
    {
        [SerializeField] protected int Step = 100;
        [SerializeField] protected int Delta = 0;
        protected int Z;
        protected Transform Transform;

        protected override void EEAwake()
        {
            base.EEAwake();
            Transform = transform;
        }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            Z = - (int) (Transform.position.y * Step) + Delta;
            SetZ();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (!Application.isPlaying) SetZ();
        }
#endif
        protected abstract void SetZ ();
    }
}