using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Network.Net.Base;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Sirenix.OdinInspector;

namespace Project.EntenEller.Base.Scripts.Network.Net
{
    public class EEServerPeerManager : EEBehaviour
    {
        [ReadOnly] public List<EEPeer> Peers = new List<EEPeer>();
        
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EENetServer>().ClientConnectedEvent += Connected;
            GetSelf<EENetServer>().ClientDisconnectedEvent += Disconnected;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EENetServer>().ClientConnectedEvent -= Connected;
            GetSelf<EENetServer>().ClientDisconnectedEvent -= Disconnected;
        }

        private void Connected(EEPeer peer)
        {
            Peers.Add(peer);
        }

        private void Disconnected(EEPeer peer)
        {
            Peers.Remove(peer);
        }
    }
}
