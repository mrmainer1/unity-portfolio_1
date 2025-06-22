using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Libraries.LiteNet
{
    public class EELiteNetServer : EENetServer, INetEventListener
    {
        private NetManager server;

        public override void On()
        {
            server = new NetManager(this);
            server.Start(Port);
            base.On();
        }

        public override void Off()
        {
            base.Off();
            server.Stop();
        }

        public void OnPeerConnected(NetPeer peer)
        {
            AddClient(EELiteNetPeer.Convert(peer));
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            RemoveClient(EELiteNetPeer.Convert(peer));
        }
        
        private void Update()
        {
            server?.PollEvents();
        }
        
        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
        {
            var bytes = new byte[reader.AvailableBytes];
            reader.GetBytes(bytes, reader.AvailableBytes);
            Receive(EELiteNetPeer.Convert(peer), EEByteSerializer.Deserialize<EEPacket>(bytes));
        }
        
        public override void Send(EEPeer peer, EEPacket packet, EEDeliveryType deliveryType)
        {
            (peer as EELiteNetPeer).NetPeer.Send(EEByteSerializer.Serialize(packet), EELiteNetDeliveryConverter.DeliveryTypeConverter.Convert(deliveryType));
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError) {}
        
        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType) {}
        public void OnNetworkLatencyUpdate(NetPeer peer, int latency) {}

        public void OnConnectionRequest(ConnectionRequest request)
        {
            request.Accept();
        }
    }
}
