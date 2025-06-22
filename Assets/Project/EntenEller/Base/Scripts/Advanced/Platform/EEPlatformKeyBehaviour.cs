using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Dictionaries;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Advanced.Platform
{
    public abstract class EEPlatformKeyBehaviour : EEBehaviourUpdate
    {
        [SerializeField] protected EEDictionary<RuntimePlatform, string> PlatformKeys;
        
        protected string GetCurrentPlatformKey()
        {
            return PlatformKeys.Dictionary[Application.platform];
        }
    }
}
