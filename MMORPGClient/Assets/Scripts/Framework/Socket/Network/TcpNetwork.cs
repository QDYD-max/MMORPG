using System.Net;
using System.Net.Sockets;
using UnityEditor.Media;
using UnityEngine;

namespace Network
{
    public class TcpNetwork
    {
        public TcpClient TcpClient = null;
        public NetworkStream NetworkStream = null;

        public TcpNetwork()
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes("aaa");
            TcpClient = new TcpClient("192.168.43.130", 8888);
            NetworkStream = TcpClient.GetStream();
            NetworkStream.Write(data, 0, data.Length);
        }

        void Send(string message)
        {
            byte[] data = System.Text.Encoding.Default.GetBytes(message);
        }
        

        byte[] Read()
        {
            return null;
        }
        
    }
}
