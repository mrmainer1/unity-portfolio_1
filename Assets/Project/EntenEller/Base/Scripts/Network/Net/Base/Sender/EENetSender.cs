using System;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Pool;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Sender
{
    public static class EENetSender
    {
        private static readonly EEPacket packet = new EEPacket();
        
        public static void Send(EEPeer peer, Type receiver, object data, EENetPoolObject poolObject, EEDeliveryType delivery, bool isForServer)
        {
            packet.ScriptName = receiver.FullName;
            packet.IsForServer = isForServer;
            packet.Data = data;
            
            if (poolObject)
            {
                var (i, j) = EESingleton.Get<EENetPoolManager>().GetIndexes(poolObject);
                packet.I = i;
                packet.J = j;
            }

            if (isForServer)
            {
                EESingleton.Get<EENetClient>().Send(packet, delivery);
            }
            else
            {
                EESingleton.Get<EENetServer>().Send(peer, packet, delivery);
            }
        }
    }
}
