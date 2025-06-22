using System.ComponentModel;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Receiver
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IEENetReceiverClient
    {
        void ReceiveAsClient(object data);
    }
}