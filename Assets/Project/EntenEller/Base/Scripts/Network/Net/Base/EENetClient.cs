using System;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Debugs;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Notifiers;
using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base
{
    [ExecutionOrder(-9998)]
    public abstract class EENetClient : EEBehaviourUpdate
    {
        [ReadOnly] public EEPeer ServerPeer;
        
        [SerializeField] protected bool IsConnectToLocalServer;
        [ShowIf("@!IsConnectToLocalServer")] public string Address;
        public int Port = 6677;
        
        public event Action ConnectedEvent;
        public event Action DisconnectedEvent;
        public EENotifier ConnectNotifier;
        public EENotifier DisconnectNotifier;
        
        public event Action<EEPacket> ReceivedMessageEvent;

        [ReadOnly] public bool IsConnected;

        public abstract void TryToConnect();

        protected void Connect()
        {
            EEDebug.Log(EEDebugTag.Client, "Connected!");
            IsConnected = true;
            ConnectedEvent.Call();
            ConnectNotifier.Notify();
        }
        
        protected void Disconnect()
        {
            EEDebug.Log(EEDebugTag.Client, "Disconnected!");
            IsConnected = false;
            ServerPeer = null;
            DisconnectedEvent.Call();
            DisconnectNotifier.Notify();
        }
        
        protected void Receive(EEPacket data)
        {
#if DEBUG
            EEDebug.Log(EEDebugTag.ClientReceive, data.ScriptName + " == " + EEJSON.Serialize(data.Data));
#endif
            ReceivedMessageEvent.Call(data);
        }

        public abstract void Send(EEPacket packet, EEDeliveryType deliveryType);
    }
}
