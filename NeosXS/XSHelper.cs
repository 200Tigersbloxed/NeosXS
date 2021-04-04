using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSNotifications;
using XSNotifications.Enum;

namespace NeosXS
{
    class XSHelper
    {
        public static void SendNotification(int messageType, string title, string content)
        {
            XSNotification notification = new XSNotification
            {
                MessageType = GetMessageTypeEnum(messageType),
                Title = title,
                Content = content,
                SourceApp = "Neos"
            };
            XSNotifier xsn = new XSNotifier();
            xsn.SendNotification(notification);
        }

        private static XSMessageType GetMessageTypeEnum(int messageType)
        {
            XSMessageType xsmt = XSMessageType.Notification;
            switch (messageType)
            {
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
