using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace NeosXS
{
    class XSHelper
    {
        public static void SendNotification(string title, string content)
        {
            IPAddress broadcastIP = IPAddress.Parse("127.0.0.1");
            Socket broadcastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint endPoint = new IPEndPoint(broadcastIP, 42069);

            XSOMessage msg = new XSOMessage
            {
                messageType = 1,
                title = title,
                content = content
            };

            byte[] byteBuffer = JsonSerializer.SerializeToUtf8Bytes(msg);
            broadcastSocket.SendTo(byteBuffer, endPoint);
        }

        private struct XSOMessage
        {
            #pragma warning disable IDE1006 // Naming Styles
            public int messageType { get; set; }
            public int index { get; set; }
            public float volume { get; set; }
            public string audioPath { get; set; }
            public float timeout { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public string icon { get; set; }
            public float height { get; set; }
            public float opacity { get; set; }
            public bool useBase64Icon { get; set; }
            public string sourceApp { get; set; }
        }
    }
}
