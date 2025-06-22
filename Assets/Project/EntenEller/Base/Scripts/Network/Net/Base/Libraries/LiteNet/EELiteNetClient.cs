using System;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Libraries.LiteNet
{
    public class EELiteNetClient : EENetClient, INetEventListener
    {
        private NetManager client;

        public override void TryToConnect()
        {
            client = new NetManager(this);
            client.Start();
            if (IsConnectToLocalServer) Address = "127.0.0.1";
            client.Connect(Address, Port, string.Empty);
        }

        public void OnPeerConnected(NetPeer peer)
        {
            ServerPeer = EELiteNetPeer.Convert(peer);
            ConnectNotifier.Notify();
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Disconnect();
        }

        private void Update()
        {
            client?.PollEvents();
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader,
            byte channelNumber, DeliveryMethod deliveryMethod)
        {
            var bytes = new byte[reader.AvailableBytes];
            reader.GetBytes(bytes, reader.AvailableBytes);
            Receive(EEByteSerializer.Deserialize<EEPacket>(bytes));
        }
        
        public override void Send(EEPacket packet, EEDeliveryType deliveryType)
        {
            var peer = ServerPeer as EELiteNetPeer;
            peer.NetPeer.Send(EEByteSerializer.Serialize(packet), EELiteNetDeliveryConverter.DeliveryTypeConverter.Convert(deliveryType));    
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError) {}
        
        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType) {}

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
            print(latency);
        }
        public void OnConnectionRequest(ConnectionRequest request) {}
    }
}