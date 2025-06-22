using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Events
{
    public class EEUnityEventClientConnectedToServer : EEBehaviour
    {
        [SerializeField] private EENotifier ClientOn;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            if (EESingleton.Get<EENetClient>().IsConnected)
            {
                ClientOn.Notify();
            }
            else
            {
                EESingleton.Get<EENetClient>().ConnectedEvent += Connect;

                void Connect()
                {
                    if (enabled) ClientOn.Notify();
                }
            }
        }
    }
}