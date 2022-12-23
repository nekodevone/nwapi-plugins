namespace Padoru.API.Features.Plugins
{
    /// <summary>
    /// Интерфейс загрузчика плагина 
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    public abstract class LoaderBase<TConfig> : IConfig
    {
        /// <summary>
        /// Конфигурация плагина
        /// </summary>
        // Bruh 
        public TConfig Config;
    }
}