#pragma warning disable IDE1006 // Naming Styles
using System.Net;
using System;
using XSNotifications;
using XSNotifications.Enum;


namespace NeosXS
{
    public class XSHelper
    {
        public static void SendNotification(int Port, string title, string content, int messageType = 0, int index = 0, float timeout = 0.5f, float height = 175, 
            float opacity = 1, float volume = 0.7f, string audioPath = "", bool useBae64Icon = false, string icon = "", string sourceApp = "")
        {
            new XSNotifier().SendNotification(new XSNotification()
            {
                MessageType = CastMessageType(messageType),
                Index = index,
                Timeout = timeout,
                Height = height,
                Opacity = opacity,
                Volume = volume,
                AudioPath = audioPath,
                Title = title,
                Content = content,
                UseBase64Icon = useBae64Icon,
                Icon = icon,
                SourceApp = sourceApp
            });
        }

        private static long AddressToInt(string ipAddress)
        {
            // Credits: https://stackoverflow.com/questions/461742/how-to-convert-an-ipv4-address-into-a-integer-in-c
            var address = IPAddress.Parse(ipAddress);
            byte[] bytes = address.GetAddressBytes();

            // flip big-endian(network order) to little-endian
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt32(bytes, 0);
        }

        private static XSMessageType CastMessageType(int messageType)
        {
            XSMessageType xsmt = XSMessageType.Notification;
            switch (messageType)
            {
                case 0:
                    xsmt = XSMessageType.Notification;
                    break;
                case 1:
                    xsmt = XSMessageType.Notification;
                    break;
                case 2:
                    xsmt = XSMessageType.MediaPlayer;
                    break;
            }

            return xsmt;
        }
    }
}
