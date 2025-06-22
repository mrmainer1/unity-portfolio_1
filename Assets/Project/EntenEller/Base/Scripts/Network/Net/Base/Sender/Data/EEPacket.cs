using System;

namespace Project.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data
{
    [Serializable]
    public class EEPacket
    {
        public int I = -1, J = -1;
        public string ScriptName;
        public bool IsForServer;
        public object Data;
    }
}