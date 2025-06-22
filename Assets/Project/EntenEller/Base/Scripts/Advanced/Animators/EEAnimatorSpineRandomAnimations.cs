using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Animators
{
    public class EEAnimatorSpineRandomAnimations : EEBehaviour
    {
        #if SPINE
        
        [SerializeField] private List<RandomAnimation> animations;

        public void Change()
        {
            var r = animations.GetRandom();
            if (r.IsLoop) GetSelf<EEAnimatorSpine>().ChangeToLoop(r.Name);
            else GetSelf<EEAnimatorSpine>().Change(r.Name);
        }

        [Serializable]
        public class RandomAnimation
        {
            public string Name;
            public bool IsLoop;
        }
        
        #endif
    }
}
