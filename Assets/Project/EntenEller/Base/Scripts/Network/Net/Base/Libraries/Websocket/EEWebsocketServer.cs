using System.Collections.Generic;
using System.Linq;
using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket
{
    public class EEWebsocketServer : EENetServer
    {
        private WebSocketServer server;
        private HashSet<string> activeUsers = new HashSet<string>();
        public static List<(EEPeer, EEPacket)> Cache = new List<(EEPeer, EEPacket)>();

        public override void On()
        {
            server = new WebSocketServer(System.Net.IPAddress.Any, Port);
            server.AddWebSocketService<ServerController>("/");
            server.Start();
            base.On();
        }

        protected override void EEUpdate()
        {
            base.EEUpdate();
            
            if (server == null) return;
            
            var services = server.WebSocketServices;
            var sessions = services.Hosts.First().Sessions;
            var ids = sessions.ActiveIDs.ToArray();

            foreach (var id in ids)
            {
                if (activeUsers.Contains(id)) continue;
                activeUsers.Add(id);
                var peer = EEWebsocketPeer.Convert(id);
                AddClient(peer);
            }
                    
            foreach (var id in activeUsers.ToArray())
            {
                if (ids.Contains(id)) continue;
                activeUsers.Remove(id);
                var peer = EEWebsocketPeer.Convert(id);
                EEWebsocketPeer.Kick(id);
                RemoveClient(peer);
            }

            while (Cache.Count != 0)
            {
                var (peer, packet) = Cache[0];
                Receive(peer, packet);
                Cache.RemoveAt(0);
            }
        }

        public override void Send(EEPeer peer, EEPacket packet, EEDeliveryType deliveryType)
        {
            var services = server.WebSocketServices;
            var sessions = services.Hosts.First().Sessions;
            sessions.SendTo(EEByteSerializer.Serialize(packet), peer.ID);
        }

        public class ServerController : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs msg)
            {
                base.OnMessage(msg);
                var packet = EEByteSerializer.Deserialize<EEPacket>(msg.RawData);
                var peer = EEWebsocketPeer.Convert(ID);
                Cache.Add((peer, packet));
            }
        }
    }
}
