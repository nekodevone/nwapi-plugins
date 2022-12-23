using Padoru.Donate.API.Enums;
using PluginAPI.Core;
using PluginAPI.Core.Interfaces;

namespace Padoru.Donate.Factory
{
    public class DonatePlayer : Player
    {
        public PrivilegeType[] Privileges { get; set; }
        
        public string Badge { get; set; }

        public DonatePlayer(IGameComponent component) : base(component)
        {
        }
    }
}