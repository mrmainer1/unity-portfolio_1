using System.Collections.Generic;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket
{
    public class EEWebsocketPeer : EEPeer
    {
        private static readonly Dictionary<string, EEWebsocketPeer> peers = new Dictionary<string, EEWebsocketPeer>();
        
        public void Set(string id)
        {
            ID = id;
        }
        
        public static EEPeer Convert(string id)
        {
            if (peers.TryGetValue(id, out var peer)) return peer;
            peer = new EEWebsocketPeer();
            peer.Set(id);
            peers.Add(id, peer);
            return peer;
        }
        
        public static void Kick(string id)
        {
            peers.Remove(id);
        }
    }
}
