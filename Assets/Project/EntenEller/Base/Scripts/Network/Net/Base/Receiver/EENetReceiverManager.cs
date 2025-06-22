using System;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Pool;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Project.EntenEller.Base.Scripts.Patterns.Pool;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Receiver
{
    public abstract class EENetReceiverManager : EEBehaviour
    {
        protected static void Receive(EEPeer peer, EEPacket packet)
        {
            IEENetReceiverClient iClient = null;
            IEENetReceiverServer iServer = null;
            
            var type = Type.GetType(packet.ScriptName);
            
            if (packet.I != -1)
            {
                var poolObject = EEPool.GetFree(EESingleton.Get<EENetPoolManager>().GetOrigin(packet.I));
                if (packet.IsForServer)
                {
                    iServer = (IEENetReceiverServer) poolObject.GetSelf(type);
                }
                else
                {
                    iClient = (IEENetReceiverClient) poolObject.GetSelf(type);
                }
            }
            else
            {
                var component = EESingleton.Get(type);
                if (packet.IsForServer)
                {
                    iServer = (IEENetReceiverServer) component;
                }
                else
                {
                    iClient = (IEENetReceiverClient) component;
                }
            }
            
            if (packet.IsForServer)
            {
                iServer.ReceiveAsServer(peer, packet.Data);
            }
            else
            {
                iClient.ReceiveAsClient(packet.Data);
            }
        }
    }
}
