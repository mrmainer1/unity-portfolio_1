using System;
using Project.EntenEller.Base.Scripts.Advanced.Debugs;
using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Libraries.LiteNet;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Pool;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data
{
    public static class EEPacketToServer
    {
        public static void Send(Type type, object data = null, EENetPoolObject netPoolObject = null,  EEDeliveryType delivery = EEDeliveryType.Unreliable)
        {
#if DEBUG
            EEDebug.Log(EEDebugTag.ClientSend, type + " == " + EEJSON.Serialize(data));
#endif
            EENetSender.Send(EESingleton.Get<EENetClient>().ServerPeer, type, data, netPoolObject, delivery, true);
        }
    }
}