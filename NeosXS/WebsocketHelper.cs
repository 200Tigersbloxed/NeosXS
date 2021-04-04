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

        public int ComponentsEnabled = 0;

        public WebSocketServer StartSocket()
        {
            if (!IsSocketServerListening)
            {
                // Maybe we don't need to check components if the socket isn't connected
                /*
                if(ComponentsEnabled <= 0)
                {
                    wssv = new WebSocketServer(host + ":" + port.ToString());
                    wssv.AddWebSocketService<NeosXSWSB>("/NeosXS");
                    wssv.Start();
                    IsSocketServerListening = true;
                    LogHelper.Debug("Websocket started on " + host + ":" + port + ".");
                }
                else
                {
                    LogHelper.Warn("Sockets are already on and Multiple Components may be using it. Not starting the socket.");
                }
                */

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
    }

    public class NeosXSWSB : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data;
            LogHelper.Debug("[DEBUG] OnMessage: " + msg);
            var stringsplit = msg.Split('/');
            messageType msgType = messageTypeToEnum(stringsplit[0]);
            string username = stringsplit[1];
            string extra1 = stringsplit[2];

            switch (msgType)
            {
                case messageType.UserJoined:
                    NotificationSender.OnUserJoined(username, extra1);
                    break;
                case messageType.UserLeft:
                    NotificationSender.OnUserLeft(username);
                    break;
                case messageType.UserPresentInWorld:
                    NotificationSender.UserPresentInWorldChanged(username, extra1.ToLower() == "true");
                    break;
                case messageType.UserPresentInHeadset:
                    NotificationSender.UserPresentInHeadsetChanged(username, extra1.ToLower() == "true");
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
            }

            return msgType;
        }
    }
}
