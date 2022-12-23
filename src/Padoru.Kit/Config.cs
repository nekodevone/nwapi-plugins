using System.ComponentModel;
using Padoru.API.Features.Plugins;

namespace Padoru.Kit
{
    public sealed class Config : IConfig
    {
        [Description("Включён ли плагин или нет")]
        public bool IsEnabled { get; set; } = true;

        [Description("Убрать SCP-2536")]
        public bool Remove2536 { get; set; } = false;
    }
}