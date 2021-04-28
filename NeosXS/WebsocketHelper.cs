using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace NeosXS
{
    public static class WebsocketHelper
    {
        public static WebSocketServer wssv;
        public static bool IsSocketServerListening = false;
        public static string host = "ws://localhost";
        public static int port = 6875;
        public static int XSOPort = 42069;

        public static int ComponentsEnabled = 0;

        public static WebSocketServer StartSocket()
        {
            if (!IsSocketServerListening)
            {
                wssv = new WebSocketServer(host + ":" + port.ToString());
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

        public static void StopSocket()
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

        public static WebSocketServer RestartSocket()
        {
            StopSocket();
            WebSocketServer wssvLOCAL = StartSocket();
            return wssvLOCAL;
        }
    }

    public class NeosXSWSB : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            LogHelper.Debug("[NEOSXSWSB] XSOPortforWSB: " + WebsocketHelper.XSOPort.ToString());
            var msg = e.Data;
            LogHelper.Debug("[SOCKET] OnMessage: " + msg);
            var stringsplit = msg.Split('/');
            messageType msgType = messageTypeToEnum(stringsplit[0]);
            LogHelper.Debug("[SOCKET] msgType: " + msgType.ToString());
            string username = stringsplit[1];
            string extra1 = stringsplit[2];

            switch (msgType)
            {
                case messageType.UserJoined:
                    NotificationSender.OnUserJoined(username, extra1, WebsocketHelper.XSOPort);
                    break;
                case messageType.UserLeft:
                    NotificationSender.OnUserLeft(username, WebsocketHelper.XSOPort);
                    break;
                case messageType.UserPresentInWorld:
                    NotificationSender.UserPresentInWorldChanged(username, extra1.ToLower() == "true", WebsocketHelper.XSOPort);
                    break;
                case messageType.UserPresentInHeadset:
                    NotificationSender.UserPresentInHeadsetChanged(username, extra1.ToLower() == "true", WebsocketHelper.XSOPort);
                    break;
                case messageType.Custom:
                    XSHelper.SendNotification(WebsocketHelper.XSOPort, username, extra1);
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
