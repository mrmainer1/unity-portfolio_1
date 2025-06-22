using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Tests
{
    public class EEClientSenderNaturalNumbers : EEBehaviour
    {
        private int number;

        public void Send()
        {
            number++;
            GetSelf<EEClientSimpleSender>().SendInt(number);
        }
    }
}
