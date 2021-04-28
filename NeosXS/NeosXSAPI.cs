//#define NEOS_PLUGIN
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if NEOS_PLUGIN
using FrooxEngine;
using BaseX;
using FrooxEngine.UIX;
#endif

namespace NeosXS
{
#if NEOS_PLUGIN
    [Category("NeosXS")]
    class NeosXSAPI : Component, ICustomInspector
    {
        public readonly Sync<bool> EnableWebsocket;
        public readonly Sync<string> Host;
        public readonly Sync<int> Port;
        public readonly Sync<int> XSOPort;

        public void BuildInspectorUI(UIBuilder ui)
        {
            WorkerInspector.BuildInspectorUI(this, ui);
        }

        protected override void OnAttach()
        {
            EnableWebsocket.Value = false;
            Host.Value = "ws://localhost";
            Port.Value = 6875;
            XSOPort.Value = 42069;
            base.OnAttach();
        }

        protected override void OnEnabled()
        {
            if (!WebsocketHelper.IsSocketServerListening)
            {
                if (EnableWebsocket.Value)
                {
                    // Start the websocket 5head
                    if (EnableWebsocket.Value)
                    {
                        WebsocketHelper.StartSocket();
                    }
                }
            }
            WebsocketHelper.ComponentsEnabled = WebsocketHelper.ComponentsEnabled + 1;
            base.OnEnabled();
        }

        protected override void OnChanges()
        {
            // Update Host and Ports
            WebsocketHelper.host = Host.Value;
            WebsocketHelper.port = Port.Value;
            WebsocketHelper.XSOPort = XSOPort.Value;
            // Enable or Disable Websocket
            if (EnableWebsocket.Value)
            {
                WebsocketHelper.RestartSocket();
            } 
            else
            {
                WebsocketHelper.StopSocket();
            }
            base.OnChanges();
        }

        protected override void OnDestroy()
        {
            WebsocketHelper.ComponentsEnabled = WebsocketHelper.ComponentsEnabled - 1;
            WebsocketHelper.StopSocket();
            base.OnDestroy();
        }
    }
#else
    public class NeosXSAPI
    {
        public int XSOPortAPI = 42069;

        public void StartSocket() { WebsocketHelper.StartSocket(); }
        public void StopSocket() { WebsocketHelper.StopSocket(); }
        public void RestartSocket() { WebsocketHelper.RestartSocket(); }

        public string GetSocketHost() { return WebsocketHelper.host; }
        public int GetSocketPort() { return WebsocketHelper.port; }
        public bool IsSocketListening() { return WebsocketHelper.IsSocketServerListening; }

        public void SetSocketHost(string host) { WebsocketHelper.host = host; }
        public void SetSocketPort(int port) { WebsocketHelper.port = port; }
        public void UpdateXSOPort() { WebsocketHelper.XSOPort = XSOPortAPI; }
    }
#endif
}
