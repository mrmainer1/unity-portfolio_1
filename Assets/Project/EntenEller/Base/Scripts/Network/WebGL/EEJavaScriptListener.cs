using System;
using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Advanced.Serializations;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Project.EntenEller.Base.Scripts.Network.WebGL
{
    public class EEJavaScriptListener : EEBehaviour
    {
        public static Action<string, string> DataReceivedEvent;

        public void GetDataFromJavaScript(string data)
        {
            var json = EEJSON.Deserialize<List<string>>(data);
            DataReceivedEvent.Call(json[0], json[1]);
        }
    }
}
