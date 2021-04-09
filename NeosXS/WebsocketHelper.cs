using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace NeosXS
{
    public class WebsocketHelper
    {
        public WebSocketServer wssv;
        public bool IsSocketServerListening = false;
        public string host = "ws://localhost";
        public int port = 6875;
        public int XSOPort = 42069;

        public int ComponentsEnabled = 0;

        public WebSocketServer StartSocket()
        {
            if (!IsSocketServerListening)
            {
                wssv = new WebSocketServer(host + ":" + port.ToString());
                NeosXSWSB.XSOPortforWSB = XSOPort;
                wssv.AddWebSocketService<NeosXSWSB>("/NeosXS");
                wssv.Start();
                IsSocketServerListening = true;
                LogHelper.Debug("Websocket started on " + host + ":" + port + ".");
            }
            else
            {
                LogHelper.Warn("Websocket already running! Did you mean to use StopSocket() or RestartSocket()");
            }

            return wssv;
        }

        public void StopSocket()
        {
            if(wssv != null && IsSocketServerListening == true)
            {
                if(ComponentsEnabled <= 0)
                {
                    try
                    {
                        IsSocketServerListening = false;
                        wssv.Stop();
                        wssv = null;
                        LogHelper.Debug("Websocket Server Stopped.");
                    }
                    catch (Exception)
                    {
                        LogHelper.Error("Failed to Stop Websocket. Is it Running?");
                    }
                }
                else
                {
                    LogHelper.Warn("Multiple Components are still using the Socket, not stopping the Websocket.");
                }
            }
        }

        public WebSocketServer RestartSocket()
        {
            StopSocket();
            WebSocketServer wssvLOCAL = StartSocket();
            return wssvLOCAL;
        }

        public void UpdateWSBXSOPort()
        {
            NeosXSWSB.XSOPortforWSB = XSOPort;
        }
    }

    public class NeosXSWSB : WebSocketBehavior
    {
        public static int XSOPortforWSB = 42069;

        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data;
            LogHelper.Debug("[SOCKET] OnMessage: " + msg);
            var stringsplit = msg.Split('/');
            messageType msgType = messageTypeToEnum(stringsplit[0]);
            LogHelper.Debug("msgType: " + msgType.ToString());
            string username = stringsplit[1];
            string extra1 = stringsplit[2];

            switch (msgType)
            {
                case messageType.UserJoined:
                    NotificationSender.OnUserJoined(username, extra1, XSOPortforWSB);
                    break;
                case messageType.UserLeft:
                    NotificationSender.OnUserLeft(username, XSOPortforWSB);
                    break;
                case messageType.UserPresentInWorld:
                    NotificationSender.UserPresentInWorldChanged(username, extra1.ToLower() == "true", XSOPortforWSB);
                    break;
                case messageType.UserPresentInHeadset:
                    NotificationSender.UserPresentInHeadsetChanged(username, extra1.ToLower() == "true", XSOPortforWSB);
                    break;
                case messageType.Custom:
                    XSHelper.SendNotification(username, extra1, XSOPortforWSB);
                    break;
            }

            base.OnMessage(e);
        }

        public enum messageType
        {
            UserJoined,
            UserLeft,
            UserPresentInWorld,
            UserPresentInHeadset,
            Custom,
            Unknown
        }

        private messageType messageTypeToEnum(string messageTypeString)
        {
            messageType msgType = messageType.Unknown;
            switch (messageTypeString.ToLower())
            {
                case "userjoined":
                    msgType = messageType.UserJoined;
                    break;
                case "userleft":
                    msgType = messageType.UserLeft;
                    break;
                case "userpresentinworld":
                    msgType = messageType.UserPresentInWorld;
                    break;
                case "userpresentinheadset":
                    msgType = messageType.UserPresentInHeadset;
                    break;
                case "custom":
                    msgType = messageType.Custom;
                    break;
            }

            return msgType;
        }
    }
}
