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
            XSHelper.SendNotification(XSOPort, username + " Joined the World!", "User's Platform is: " + platform);
        }

        public static void OnUserLeft(string username, int XSOPort)
        {
            XSHelper.SendNotification(XSOPort, username + " Left the World!", RandomLeaveMessage());
        }

        public static void UserPresentInWorldChanged(string username, bool value, int XSOPort)
        {
            if (value)
                XSHelper.SendNotification(XSOPort, username + " has focused into the World", "Welcome back!");
            else
                XSHelper.SendNotification(XSOPort, username + " has unfocused from the World", "Huh, where'd you go?");
        }

        public static void UserPresentInHeadsetChanged(string username, bool value, int XSOPort)
        {
            if (value)
                XSHelper.SendNotification(XSOPort, username + " put their Headset back on", "welcome back c:");
            else
                XSHelper.SendNotification(XSOPort, username + " took off their Headset", "goodbye :c");
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
