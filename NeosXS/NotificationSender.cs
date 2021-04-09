using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeosXS
{
    /*
    UserJoined
    UserLeft
    UserPresentInWorld
    UserPresentInHeadset
    UserPlatform
    */
    public class NotificationSender
    {
        public static void OnUserJoined(string username, string platform, int XSOPort)
        {
            XSHelper.SendNotification(username + " Joined the World!", "User's Platform is: " + platform, XSOPort);
        }

        public static void OnUserLeft(string username, int XSOPort)
        {
            XSHelper.SendNotification(username + " Left the World!", RandomLeaveMessage(), XSOPort);
        }

        public static void UserPresentInWorldChanged(string username, bool value, int XSOPort)
        {
            if (value)
                XSHelper.SendNotification(username + " has focused into the World", "Welcome back!", XSOPort);
            else
                XSHelper.SendNotification(username + " has unfocused from the World", "Huh, where'd you go?", XSOPort);
        }

        public static void UserPresentInHeadsetChanged(string username, bool value, int XSOPort)
        {
            if (value)
                XSHelper.SendNotification(username + " put their Headset back on", "welcome back c:", XSOPort);
            else
                XSHelper.SendNotification(username + " took off their Headset", "goodbye :c", XSOPort);
        }

        private static string RandomLeaveMessage()
        {
            string[] LeaveMessages = { "Goodbye!", "You will be missed :c", "*crashing noises*", "Cat walked on the keyboard", "Alt+F4", "CMD+Q", "Alt-F4 on Linux?", "Everyone Laugh at them", "We do a Little Trolling -Donald John Trump", "i'm not creative with leave messages"};
            Random random = new Random();
            int index = random.Next(0, LeaveMessages.Length);

            return LeaveMessages[index];
        }
    }
}
