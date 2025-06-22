using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Textures.Other
{
    public class EERendererEndlessParallax : EEBehaviourUpdate
    {
        [SerializeField] private float speed = 1;
        private Vector2 offset = Vector2.zero;
        
        protected override void EEUpdate()
        {
            base.EEUpdate();
            offset.x = Time.time * speed;
            GetSelf<Renderer>().material.mainTextureOffset = offset;
        }
    }
}