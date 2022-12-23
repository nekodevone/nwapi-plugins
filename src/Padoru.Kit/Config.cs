using System.ComponentModel;
using Padoru.API.Features.Plugins;
using UnityEngine;

namespace Padoru.Kit
{
    public sealed class Config : IConfig
    {
        [Description("Включён ли плагин или нет")]
        public bool IsEnabled { get; set; } = true;

        [Description("Убрать SCP-2536")]
        public bool Remove2536 { get; set; } = false;

        [Description("Расположение башни туториалов")]
        public Vector3 TowerPosition { get; set; } = new(130.335f, 994f, 20.113f);
    }
}