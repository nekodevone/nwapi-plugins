using System.ComponentModel;

namespace Padoru.Logger.Structs
{
    public class LoggingEvents
    {
        [Description("Игрок")]
        public bool PlayerJoined { get; set; } = true;
        public bool PlayerLeft { get; set; } = true;
        public bool PlayerDamage { get; set; } = true;
        public bool PlayerBanned { get; set; } = true;
        public bool PlayerDropItem { get; set; } = true;
        public bool PlayerSearchedPickup { get; set; } = true;
        public bool PlayerEscape { get; set; } = true;
        public bool PlayerEnterPocketDimension { get; set; } = true;
        public bool PlayerExitPocketDimension { get; set; } = true;
        public bool PlayerChangeRole { get; set; } = true;
        public bool PlayerDeath { get; set; } = true;
        public bool PlayerThrowProjectile { get; set; } = true;
        public bool Scp939CreateAmnesticCloud { get; set; } = true;
        public bool PlayerOpenGenerator { get; set; } = true;
        public bool PlayerCloseGenerator { get; set; } = true;
        public bool PlayerInteractScp330 { get; set; } = true;
        
        [Description("Сервер")]
        public bool WaitingForPlayers { get; set; } = true;
        public bool RoundStart { get; set; } = true;
        public bool RoundEnd { get; set; } = true;
        public bool RoundRestart { get; set; } = true;
        public bool RemoteAdminCommandExecuted { get; set; } = true;
        
        [Description("Альфа-Боеголовка")]
        public bool WarheadStart { get; set; } = true;
        public bool WarheadStop { get; set; } = true;
        public bool WarheadDetonation { get; set; } = true;
        
        [Description("Сцп")]
        public bool Scp049ResurrectBody { get; set; } = true;
        public bool Scp106Stalking { get; set; } = true;
        public bool Scp914Activate { get; set; } = true;
        public bool Scp914KnobChange { get; set; } = true;
        public bool Scp079UseTesla { get; set; } = true;
        public bool Scp079LockdownRoom { get; set; } = true;
        public bool Scp079BlackoutZone { get; set; } = true;
        public bool Scp079LevelUpTier { get; set; } = true;
    }
}