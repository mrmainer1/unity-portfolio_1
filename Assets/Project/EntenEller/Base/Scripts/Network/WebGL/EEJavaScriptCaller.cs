using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.WebGL
{
    public class EEJavaScriptCaller : EEBehaviour
    {
        public static void SendDataFromUnity(string function, string data)
        {
#pragma warning disable CS0618
            Application.ExternalCall(function, data);
#pragma warning restore CS0618
        }
    }
}