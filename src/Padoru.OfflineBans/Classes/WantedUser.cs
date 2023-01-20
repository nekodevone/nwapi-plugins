namespace Padoru.OffBans.Classes
{
    using Padoru.OfflineBans.Classes;
    using System.IO;
    using PlayerAPI = PluginAPI.Core.Player;
    public class WantedUser
    {
        public string ID { get; set; }
        public long BanTime { get; set; }
        public string Reason { get; set; }

        public WantedUser(string steamID, long banTime, string reason)
        {
            ID = steamID;
            BanTime = banTime;
            Reason = reason;
        }

        public static void Ban(PlayerAPI player)
        {
            string path = Tools.filepath + $"\\{player.UserId}.json";
            StreamReader r = new StreamReader(path);
            string json = r.ReadToEnd();

            WantedUser user = Newtonsoft.Json.JsonConvert.DeserializeObject<WantedUser>(json);
            player.Ban(user.Reason, user.BanTime);
            File.Delete(path);
        }
    }
}
