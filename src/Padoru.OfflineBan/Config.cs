using Padoru.API.Features.Plugins;
using System.ComponentModel;

namespace Padoru.OfflineBan
{
    public sealed class Config : IConfig
    {
        [Description("Включён ли плагин или нет")]
        public bool IsEnabled { get; set; } = true;
    }
}