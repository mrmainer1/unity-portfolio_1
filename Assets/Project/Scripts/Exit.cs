using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using Project.EntenEller.Base.Scripts.Network.WebGL;

namespace Project.Scripts
{
    public class Exit : EEBehaviour
    {
        public void Call()
        {
            EEJavaScriptCaller.SendDataFromUnity("UnityExit", "");
        }
    }
}
