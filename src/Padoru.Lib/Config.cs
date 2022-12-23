using System.ComponentModel;
using Padoru.API.Features.Plugins;

namespace Padoru.Lib
{
    public sealed class Config : IConfig
    {
        [Description("Плагин невозможно отключить")]
        public bool IsEnabled
        {
            get => true;
            set { }
        }
    }
}