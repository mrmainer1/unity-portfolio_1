using System.Collections.Generic;
using LiteNetLib;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Libraries.LiteNet
{
    public class EELiteNetPeer : EEPeer
    {
        public NetPeer NetPeer;
        private static readonly Dictionary<NetPeer, EELiteNetPeer> peers = new Dictionary<NetPeer, EELiteNetPeer>();

        public void Set(NetPeer netPeer)
        {
            NetPeer = netPeer;
            ID = netPeer.Id.ToString();
        }
        
        public static EEPeer Convert(NetPeer netPeer)
        {
            if (peers.TryGetValue(netPeer, out var peer)) return peer;
            peer = new EELiteNetPeer();
            peer.Set(netPeer);
            peers.Add(netPeer, peer);
            return peer;
        }
        
        public static void Kick(NetPeer netPeer)
        {
            peers.Remove(netPeer);
        }
    }
}
