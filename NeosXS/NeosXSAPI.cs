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
        private WebsocketHelper sockethelper = new WebsocketHelper();

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
            if (!sockethelper.IsSocketServerListening)
            {
                if (EnableWebsocket.Value)
                {
                    // Start the websocket 5head
                    if (EnableWebsocket.Value)
                    {
                        sockethelper.StartSocket();
                    }
                }
            }
            sockethelper.ComponentsEnabled = sockethelper.ComponentsEnabled + 1;
            base.OnEnabled();
        }

        protected override void OnChanges()
        {
            // Update Host and Ports
            sockethelper.host = Host.Value;
            sockethelper.port = Port.Value;
            sockethelper.XSOPort = XSOPort.Value;
            // Enable or Disable Websocket
            if (EnableWebsocket.Value)
            {
                sockethelper.RestartSocket();
            } 
            else
            {
                sockethelper.StopSocket();
            }
            base.OnChanges();
        }

        protected override void OnDestroy()
        {
            sockethelper.ComponentsEnabled = sockethelper.ComponentsEnabled - 1;
            sockethelper.StopSocket();
            base.OnDestroy();
        }
    }
#else
    public class NeosXSAPI
    {
        public int XSOPortAPI = 42069;
        private WebsocketHelper sockethelper = new WebsocketHelper();

        public void StartSocket() { sockethelper.StartSocket(); }
        public void StopSocket() { sockethelper.StopSocket(); }
        public void RestartSocket() { sockethelper.RestartSocket(); }

        public string GetSocketHost() { return sockethelper.host; }
        public int GetSocketPort() { return sockethelper.port; }
        public bool IsSocketListening() { return sockethelper.IsSocketServerListening; }

        public void SetSocketHost(string host) { sockethelper.host = host; }
        public void SetSocketPort(int port) { sockethelper.port = port; }
        public void SetSocketXSOPort(int xsoport) { XSOPortAPI = xsoport; }
        public void UpdateXSOPort() { sockethelper.XSOPort = XSOPortAPI; }
    }
#endif
}
