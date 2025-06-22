using System;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours;
using Project.EntenEller.Base.Scripts.Advanced.Behaviours.Loop;
using Project.EntenEller.Base.Scripts.Advanced.Debugs;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Sirenix.OdinInspector;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base
{
    [ExecutionOrder(-9999)]
    public abstract class EENetServer : EEBehaviourUpdate
    {
        public int Port = 6677;
        [ReadOnly] public bool IsActive;
        
        public event Action OnEvent;
        public event Action OffEvent;
        
        public event Action<EEPeer> ClientConnectedEvent;
        public event Action<EEPeer> ClientDisconnectedEvent;
        
        public event Action<EEPeer, EEPacket> ReceiveMessageEvent;

        public virtual void On()
        {
            EEDebug.Log(EEDebugTag.Server, "ON!");
            IsActive = true;
            OnEvent.Call();
        }

        public virtual void Off()
        {
            EEDebug.Log(EEDebugTag.Server, "OFF!");
            IsActive = false;
            OffEvent.Call();
        }

        protected void AddClient(EEPeer peer)
        {
            EEDebug.Log(EEDebugTag.Server, "Add client " + peer.ID);
            ClientConnectedEvent.Call(peer);
        }
        
        protected void RemoveClient(EEPeer peer)
        {
            EEDebug.Log(EEDebugTag.Server, "Remove client " + peer.ID);
            ClientDisconnectedEvent.Call(peer);
        }
        
        public void Receive(EEPeer peer, EEPacket data)
        {
#if DEBUG
            EEDebug.Log(EEDebugTag.ServerReceive, data.ScriptName + " == " + EEJSON.Serialize(data));
#endif
            ReceiveMessageEvent.Call(peer, data);
        }
        
        public abstract void Send(EEPeer peer, EEPacket packet, EEDeliveryType deliveryType);
    }
}
