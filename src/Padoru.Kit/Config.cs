using System.ComponentModel;
using Padoru.API.Features.Plugins;
using Padoru.API.Structs;

namespace Padoru.Kit
{
    public sealed class Config : IConfig
    {
        [Description("Включён ли плагин или нет")]
        public bool IsEnabled { get; set; } = true;

        [Description("Расположение башни туториалов")]
        public SerializedVector3 TowerPosition { get; set; } = new(130.335f, 994f, 20.113f);
    }
}