namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Receiver
{
    public interface IEENetReceiverServer
    {
        void ReceiveAsServer(EEPeer peer, object data);
    }
}