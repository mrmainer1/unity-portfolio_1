using System;
using Project.EntenEller.Base.Scripts.Advanced.Debugs;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Tests
{
    public class EENetPing : EEBehaviour, IEENetReceiverClient, IEENetReceiverServer
    {
        public void Call()
        {
            EEPacketToServer.Send(typeof(EENetPing), DateTime.Now);
        }
        
        public void ReceiveAsClient(object data)
        {
            EEDebug.Log(EEDebugTag.Default, (DateTime.Now - (DateTime) data).Milliseconds);
        }

        public void ReceiveAsServer(EEPeer peer, object data)
        {
            EEPacketToClient.Send(peer, typeof(EENetPing), data);
        }
    }
}
