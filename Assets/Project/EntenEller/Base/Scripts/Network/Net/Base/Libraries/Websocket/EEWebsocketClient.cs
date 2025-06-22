using System;
using System.Globalization;
using BestHTTP.WebSocket;
using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket
{
    public class EEWebsocketClient : EENetClient
    {
        private WebSocket webSocket;
        
        public override void TryToConnect()
        {
            if (IsConnectToLocalServer) Address = "ws://127.0.0.1";
            Address += ":" + Port;
            webSocket = new WebSocket(new Uri(Address));
            webSocket.Open();
            webSocket.OnOpen += OnOpen;
            webSocket.OnBinary += OnBinary;
            webSocket.OnClosed += OnClosed;
            webSocket.OnError += OnError;
        }
        
        private void OnOpen(WebSocket websocket)
        {
            Connect();
        }
        
        private void OnClosed(WebSocket websocket, ushort code, string message)
        {
            webSocket.OnOpen -= OnOpen;
            webSocket.OnBinary -= OnBinary;
            webSocket.OnClosed -= OnClosed;
            webSocket.OnError -= OnError;
            webSocket.Close();
            Disconnect();
        }
        
        private void OnBinary(WebSocket websocket, byte[] data)
        {
            Receive(EEByteSerializer.Deserialize<EEPacket>(data));
        }
        
        public override void Send(EEPacket packet, EEDeliveryType deliveryType)
        {
            webSocket.Send(EEByteSerializer.Serialize(packet));
        }

        private void OnError(WebSocket websocket, string reason) {}
    }
}
