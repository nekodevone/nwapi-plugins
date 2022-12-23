namespace Padoru.API.Features.Plugins
{
    /// <summary>
    /// Интерфейс конфигурации плагина
    /// </summary>
    public interface IConfig
    {
        bool IsEnabled { get; set; }
    }
}