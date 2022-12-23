using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Padoru.API;
using Padoru.API.Features.Plugins;
using Padoru.Donate.API.Enums;
using PluginAPI.Helpers;

namespace Padoru.Donate
{
    public sealed class Config : IConfig
    {
        [Description("Включены ли донаты, или нет")]
        public bool IsEnabled { get; set; } = true;

        [Description("Путь к файлу с привилегиями")]
        public string PrivilegePath { get; set; } = Path.Combine(Paths.PluginAPI, "privileges.json");

        [Description("Какие привилегии имеют резервный слот")]
        public PrivilegeType[] ReservedSlots { get; set; } =
        {
            PrivilegeType.ReservedSlot,
            PrivilegeType.PatronTier2,
            PrivilegeType.PatronTier3
        };

        [Description("Какие привилегии имеют Overwatch")]
        public PrivilegeType[] Overwatch { get; set; } =
        {
            PrivilegeType.PatronTier2,
            PrivilegeType.PatronTier3
        };

        [Description("Множитель опыта SCP-079")]
        public Dictionary<PrivilegeType, float> Scp079XpBoost { get; set; } = new()
        {
            [PrivilegeType.PatronTier1] = 10,
            [PrivilegeType.PatronTier2] = 25,
            [PrivilegeType.PatronTier3] = 40
        };

        [Description("Доп. шанс на побег из КИ SCP-106")]
        public Dictionary<PrivilegeType, int> PocketEscapeChange { get; set; } = new()
        {
            [PrivilegeType.PatronTier1] = 10,
            [PrivilegeType.PatronTier2] = 25,
            [PrivilegeType.PatronTier3] = 40
        };

        [Description("Доп. время в интеркоме")]
        public Dictionary<PrivilegeType, uint> IntercomExtraTime { get; set; } = new()
        {
            [PrivilegeType.PatronTier1] = 20,
            [PrivilegeType.PatronTier2] = 30,
            [PrivilegeType.PatronTier3] = 40
        };

        [Description("Текст плашки")]
        public Dictionary<PrivilegeType, string> BadgeText { get; set; } = new()
        {
            [PrivilegeType.PatronTier1] = "FUNCLUB GOLD PATRON",
            [PrivilegeType.PatronTier2] = "FUNCLUB DIAMOND PATRON",
            [PrivilegeType.PatronTier3] = "FUNCLUB PLATINUM PATRON"
        };

        [Description("Цвет плашки")]
        public Dictionary<PrivilegeType, string> BadgeColor { get; set; } = new()
        {
            [PrivilegeType.PatronTier1] = "pumpkin",
            [PrivilegeType.PatronTier2] = "cyan",
            [PrivilegeType.PatronTier3] = "magenta"
        };

        [Description("Цвет приветственного сообщения")]
        public Dictionary<PrivilegeType, string> WelcomeColor { get; set; } = new()
        {
            [PrivilegeType.PatronTier1] = Color.Yellow,
            [PrivilegeType.PatronTier2] = Color.Blue,
            [PrivilegeType.PatronTier3] = Color.Red
        };
    }
}