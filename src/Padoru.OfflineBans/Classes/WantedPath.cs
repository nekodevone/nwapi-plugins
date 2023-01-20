namespace Padoru.OfflineBans.Classes
{
    using PluginAPI.Helpers;

    public static class WantedPath
    {
        public static string dirname { get; } = "Wanted";

        public static string filepath { get; } = Paths.SecretLab + dirname;
    }
}
