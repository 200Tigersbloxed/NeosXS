using XSNotifications;
using XSNotifications.Enum;

namespace NeosXS
{
    public class XSHelper
    {
        public static void SendNotification(string title, string content)
        {
            new XSNotifier().SendNotification(new XSNotification() { Title = title, Content = content });
        }
    }
}
