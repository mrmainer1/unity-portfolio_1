using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.HTTP
{
    public class EEHTTPByteStringSender : EEHTTPByteSender
    {
        [SerializeField] private string data;

        protected override byte[] Collect()
        {
            return System.Text.Encoding.UTF8.GetBytes(data);
        }

        public void UpdateData(string newData)
        {
            data = newData;
        }
    }
}
