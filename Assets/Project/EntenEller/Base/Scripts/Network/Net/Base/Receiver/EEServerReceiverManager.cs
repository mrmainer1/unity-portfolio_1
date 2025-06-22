using Project.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Receiver
{
    public class EEServerReceiverManager : EENetReceiverManager
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            EESingleton.Get<EENetServer>().ReceiveMessageEvent += Receive;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            EESingleton.Get<EENetServer>().ReceiveMessageEvent -= Receive;
        }
    }
}