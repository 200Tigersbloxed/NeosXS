using XSNotifications;
using XSNotifications.Enum;

namespace NeosXS
{
    public class XSHelper
    {
        public static void SendNotification(string title, string content, int Port)
        {
            new XSNotifier(Port, Port).SendNotification(new XSNotification() { Title = title, Content = content });
        }
    }
}
