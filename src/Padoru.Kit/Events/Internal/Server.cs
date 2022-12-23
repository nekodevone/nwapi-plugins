﻿using Padoru.API.Features.Plugins;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace Padoru.Kit.Events.Internal
{
    public class Server : IEventHandler
    {
        [PluginEvent(ServerEventType.WaitingForPlayers)]
        public void Remove2536OnWaitingForPlayers()
        {
            if (!Plugin.Configs.Remove2536)
            {
                return;
            }

            Christmas.Scp2536.Scp2536Controller.Singleton.enabled = false;
        }
    }
}