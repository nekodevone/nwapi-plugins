using System.Net.Http;

namespace Padoru.Lib
{
    public static class Http
    {
        public static HttpClient Client { get; } = new();
    }
}