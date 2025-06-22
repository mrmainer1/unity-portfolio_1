using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Project.EntenEller.Base.Scripts.Advanced.Variables;
using Project.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.EntenEller.Base.Scripts.Network.DMX
{
    public class EEDMXSender : EEBehaviour
    {
        public string ipAddress = "2.0.0.1";
        public int universe = 0;
        public string header = "Art-Net";
        private UdpClient udpClient;
        
        public void Connect()
        {
            udpClient = new UdpClient();
        }

        public void SendRGB(Color color)
        {
            var byteColor = color.ToBytes();
            Send(byteColor.ToList());
        }
        
        public void SendHEX(string hexColor)
        {
            var color = hexColor.HexToColor();
            var byteColor = color.ToBytes();
            Send(byteColor.ToList());
        }
        
        public void Send(List<byte> data)
        {
            if (udpClient == null) Connect();
            
            var packet = new byte[18 + data.Count];
            
            Encoding.ASCII.GetBytes(header).CopyTo(packet, 0); // 7 bytes: Art-Net header
            packet[7] = 0x0; // NULL byte
            packet[8] = 0x00; // Opcode (Low byte)
            packet[9] = 0x50; // Opcode (High byte)
            packet[10] = 0x00; // Protocol version (High byte)
            packet[11] = 0x0E; // Protocol version (Low byte)
            packet[12] = 0x00; // Sequence
            packet[13] = 0x00; // Physical
            packet[14] = (byte)(universe & 0xFF); // Universe (Low byte)
            packet[15] = (byte)((universe >> 8) & 0xFF); // Universe (High byte)
            packet[16] = (byte)((data.Count >> 8) & 0xFF); // Data length (High byte)
            packet[17] = (byte)(data.Count & 0xFF); // Data length (Low byte)
            data.CopyTo(packet, 18);
            var packetDataString = string.Join(", ", packet.Select(b => b.ToString()));
            Debug.Log("Packet Data: " + packetDataString);
            udpClient.Send(packet, packet.Length, ipAddress, 6454);
        }
    }
}
