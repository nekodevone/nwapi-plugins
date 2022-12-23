using System;
using PluginAPI.Core.Factories;
using PluginAPI.Core.Interfaces;

namespace Padoru.Donate.Factory
{
    public class DonatePlayerFactory : PlayerFactory
    {
        public override Type BaseType { get; } = typeof(DonatePlayer);

        public override IPlayer Create(IGameComponent component)
        {
            return new DonatePlayer(component);
        }
    }
}