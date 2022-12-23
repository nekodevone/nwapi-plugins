using System.Net.Http;

namespace Padoru.API.Features
{
    public static class Http
    {
        public static HttpClient Client { get; } = new();
    }
}