namespace Padoru.Lib.API.Features.Plugins
{
    /// <summary>
    /// Интерфейс загрузчика плагина 
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    public interface ILoader<TConfig> : IConfig
    {
        /// <summary>
        /// Конфигурация плагина
        /// </summary>
        public TConfig Config { get; set; }
    }
}