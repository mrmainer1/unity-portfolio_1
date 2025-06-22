using System;
using System.Net;
using System.Net.Sockets;
using Project.EntenEller.Base.Scripts.Advanced.Async;
using Project.EntenEller.Base.Scripts.Advanced.Events;
using Project.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.UDP
{
    [ExecutionOrder(-9999)]
    public class UDP : MonoBehaviour
    {
        public static UDP Instance;
        public UdpClient udpClient;
        private IPEndPoint remoteEndPoint;
        public byte[] ReceivedData;
        public Action ReceivedEvent;
        [SerializeField] private string ip;
        [SerializeField] private int port;

        private void Awake()
        {
            Instance = this;
            EESingleton.Get<EEUnityThreadDispatcher>().Init();
        }

        public void On()
        {
            try
            {
                udpClient = new UdpClient(ip, port);
                remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                udpClient.BeginReceive(OnReceive, null);
                Debug.Log("UDP started: ephemeral port = " 
                          + ((IPEndPoint)udpClient.Client.LocalEndPoint).Port 
                          + " -> " + ip + ":" + port);
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        public void Off()
        {
            if (udpClient == null) return;
            udpClient.Close();
            udpClient = null;
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                ReceivedData = udpClient.EndReceive(ar, ref remoteEndPoint);
                ReceivedEvent.Call();
                udpClient.BeginReceive(OnReceive, null);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void SendData(ref byte[] data, int length)
        {
            if (udpClient == null) return;
            udpClient.Send(data, length);
        }
    }
}