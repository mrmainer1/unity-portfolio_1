using System;
using Project.EntenEller.Base.Scripts.Advanced.Debugs;
using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Pool;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data
{
    public static class EEPacketToClient
    {
        public static void Send(EEPeer client, Type type, object data = null, EENetPoolObject netPoolObject = null, EEDeliveryType delivery = EEDeliveryType.Unreliable)
        {
#if DEBUG
            EEDebug.Log(EEDebugTag.ServerSend, type + " == " + EEJSON.Serialize(data));
#endif
            EENetSender.Send(client, type, data, netPoolObject, delivery, false);
        }
    }
}