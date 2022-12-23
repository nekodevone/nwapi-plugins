using System.ComponentModel;
using Padoru.API.Features.Plugins;

namespace Padoru.Kit
{
    public class Config : IConfig
    {
        [Description("Убрать SCP-2536")]
        public bool Remove2536 { get; set; } = false;
    }
}