using PluginAPI.Helpers;
using System.IO;
using System.Text.RegularExpressions;

namespace Padoru.OfflineBans.Classes
{
    public static class Tools
    {
        public static string dirname { get; } = "Wanted";

        public static string FolderPath { get; } = Path.Combine(Paths.PluginAPI, dirname);

        public static Regex[] Regex { get; } = { new Regex(@"/^\d{17}@steam\s\d*.\s.*/"),
                                          new Regex(@"/^\d{18}@discord\s\d*.\s.*/")};

        public static Regex[] RegexDel { get; } = { new Regex(@"/^\d{18}@discord\s.*/"),
                                             new Regex(@"/^\d{17}@steam\s.*/")};
    }
}
