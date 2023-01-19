using System.ComponentModel;
using Padoru.API.Features.Plugins;
using Padoru.API.Structs;

namespace Padoru.Kit
{
    public sealed class Config : IConfig
    {
        [Description("Включён ли плагин или нет")]
        public bool IsEnabled { get; set; } = true;
    }
}