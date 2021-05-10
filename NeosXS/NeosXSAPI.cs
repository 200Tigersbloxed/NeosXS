#define NEOS_PLUGIN
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if NEOS_PLUGIN
using FrooxEngine;
using FrooxEngine.UIX;
#endif

namespace NeosXS
{
#if NEOS_PLUGIN
    [Category("NeosXS")]
    class NeosXSAPI : Component
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

    public class NeosXSAPIUserListener: SimpleUserSpawn
    {
        public Dictionary<World, CacheUserFields> CurrentUsers;
        public User LocalUser = null;
        public World w1;
        public World w2;

        public override void OnUserJoined(User user)
        {
            GetAllUsers(CurrentUsers);
            if (WebsocketHelper.IsSocketServerListening)
            {
                NotificationSender.OnUserJoined(user.UserName, user.Platform.ToString(), WebsocketHelper.XSOPort);
            }
            if (user.IsLocalUser)
            {
                LocalUser = user;
            }
            base.OnUserJoined(user);
        }

        public override void OnUserLeft(User user)
        {
            GetAllUsers(CurrentUsers);
            if (WebsocketHelper.IsSocketServerListening)
            {
                NotificationSender.OnUserLeft(user.UserName, WebsocketHelper.XSOPort);
            }
            base.OnUserLeft(user);
        }

        public void GetAllUsers(Dictionary<World, CacheUserFields> DictionaryToApply)
        {
            if(LocalUser != null)
            {
                World currentWorld = LocalUser.World;
                List<User> worldusers = null;
                currentWorld.GetUsers(worldusers);
                if(DictionaryToApply.ContainsKey(currentWorld))
                {
                    foreach(KeyValuePair<User, bool> entry in DictionaryToApply[currentWorld].WorldFocusCache)
                    {
                        if (entry.Key.World != currentWorld)
                        {
                            DictionaryToApply[currentWorld].WorldFocusCache.Remove(entry.Key);
                        }
                    }

                    foreach (KeyValuePair<User, bool> entry in DictionaryToApply[currentWorld].HeadsetFocusCache)
                    {
                        if (entry.Key.World != currentWorld)
                        {
                            DictionaryToApply[currentWorld].HeadsetFocusCache.Remove(entry.Key);
                        }
                    }
                }
                else
                {
                    CacheUserFields cuf = new CacheUserFields();
                    foreach (User user in worldusers)
                    {
                        cuf.WorldFocusCache.Add(user, user.IsPresentInWorld);
                        cuf.HeadsetFocusCache.Add(user, user.IsPresentInHeadset);
                    }
                    DictionaryToApply.Add(currentWorld, cuf);
                }
            }
            else { LogHelper.Error("LocalUser is null! Cannot get users!"); }
        }

        protected override void OnBehaviorUpdate()
        {
            // world check
            if(LocalUser != null)
            {
                if(w1 == null)
                {
                    w1 = LocalUser.World;
                }
                else
                {
                    w2 = w1;
                    w1 = LocalUser.World;
                }
            }
            else { LogHelper.Error("LocalUser is null! Cannot get users!"); }

            if(w1 != w2)
            {
                if (!w2.LocalUser.IsPresent)
                {
                    CurrentUsers.Remove(w2);
                }
            }
            // user check
            if(w1 != null)
            {
                foreach (KeyValuePair<User, bool> entry in CurrentUsers[w1].WorldFocusCache)
                {
                    User user = entry.Key;
                    if(entry.Value != user.IsPresentInWorld)
                    {
                        NotificationSender.UserPresentInHeadsetChanged(user.UserName, user.IsPresentInWorld, WebsocketHelper.XSOPort);
                        CurrentUsers[w1].WorldFocusCache[user] = user.IsPresentInWorld;
                    }
                }

                foreach(KeyValuePair<User, bool> entry in CurrentUsers[w1].HeadsetFocusCache)
                {
                    User user = entry.Key;
                    if(entry.Value != user.IsPresentInWorld)
                    {
                        NotificationSender.UserPresentInWorldChanged(user.UserName, user.IsPresentInWorld, WebsocketHelper.XSOPort);
                        CurrentUsers[w1].WorldFocusCache[user] = user.IsPresentInHeadset;
                    }
                }
            }

            base.OnBehaviorUpdate();
        }
    }

    public class CacheUserFields
    {
        public Dictionary<User, bool> WorldFocusCache;
        public Dictionary<User, bool> HeadsetFocusCache;
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
