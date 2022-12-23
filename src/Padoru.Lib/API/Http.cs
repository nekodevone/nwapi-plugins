using System.Net.Http;

namespace Padoru.Lib.API
{
    public static class Http
    {
        public static HttpClient Client { get; } = new();
    }
}