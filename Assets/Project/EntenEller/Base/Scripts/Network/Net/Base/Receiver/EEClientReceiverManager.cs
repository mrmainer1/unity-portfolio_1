using System;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Libraries.LiteNet;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Receiver
{
    public class EEClientReceiverManager : EENetReceiverManager
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            EESingleton.Get<EENetClient>().ReceivedMessageEvent += OnEEClientEventsOnReceivedMessageEvent;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            EESingleton.Get<EENetClient>().ReceivedMessageEvent -= OnEEClientEventsOnReceivedMessageEvent;
        }

        private void OnEEClientEventsOnReceivedMessageEvent(EEPacket packet)
        {
            Receive(EESingleton.Get<EENetClient>().ServerPeer, packet);
        }
    }
}