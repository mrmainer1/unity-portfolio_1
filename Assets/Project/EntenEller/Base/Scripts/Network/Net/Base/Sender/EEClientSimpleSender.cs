using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Sender
{
    public class EEClientSimpleSender : EEBehaviour
    {
        public void SendString(string data)
        {
            EEPacketToServer.Send(typeof(string), data);
        }
        
        public void SendInt(int data)
        {
            EEPacketToServer.Send(typeof(int), data);
        }
    }
}
