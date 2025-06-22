using System.Collections.Generic;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.OSC
{
    public class EEOSCSender : EEBehaviour
    {
        [SerializeField] private global::OSC osc;

        public void SendAddressOnly(string address)
        {
            Send(address, null);
        }
        
        public void SendTrue(string address)
        {
            Send(address, new List<object> {1});
        }
        
        public void SendFalse(string address)
        {
            Send(address, new List<object> {0});
        }
        
        public void Send(string address, List<object> values)
        {
            var message = new OscMessage
            {
                address = address
            };
            if (values != null)
            {
                foreach (var value in values)
                {
                    message.values.Add(value);
                }
            }
            print(message.address);
            osc.Send(message);
        }
    }
}
