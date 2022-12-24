using Padoru.API.Features.Plugins;
using Padoru.Logger.Extensions;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PlayerAPI = PluginAPI.Core.Player;

namespace Padoru.Logger.Events.Internal
{
    public class Scp049 : IEventHandler
    {
        [PluginEvent(ServerEventType.Scp049ResurrectBody)]
        public void OnScp049ResurrectBody(PlayerAPI scp049, PlayerAPI target, BasicRagdoll ragdoll)
        {
            if (!Plugin.Configs.LoggingEvents.Scp049ResurrectBody)
            {
                return;
            }

            Plugin.Sender.AddToQuene($"Игрок {scp049.GetInfo()} воскресил {target.GetInfo()}.");
        }
    }
}