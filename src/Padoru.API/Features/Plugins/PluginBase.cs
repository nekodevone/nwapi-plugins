using System.Reflection;
using PluginAPI.Core;
using PluginAPI.Events;

namespace Padoru.API.Features.Plugins
{
    /// <summary>
    /// Базовый класс плагина
    /// </summary>
    /// <typeparam name="TConfig"><see cref="TConfig">Конфигурация плагина</see></typeparam>
    public abstract class PluginBase<TConfig> where TConfig : IConfig
    {
        /// <summary>
        /// <see cref="Assembly"/> плагина
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// <see cref="ILoader"/> плагина
        /// </summary>
        public ILoader Loader { get; private set; }

        /// <summary>
        /// <see cref="IConfig"/> плагина
        /// </summary>
        public TConfig Config { get; private set; }

        /// <summary>
        /// Метод, вызываемый из загрузчика для инициализации плагина
        /// </summary>
        public void Load(ILoader loader, TConfig config)
        {
            var assembly = loader.GetType().Assembly;

            Assembly = assembly;
            Loader = loader;
            Config = config;

            if (!Config.IsEnabled)
            {
                Log.Info($"Plugin {assembly.GetName().Name} has been disabled");
                return;
            }

            try
            {
                OnLoaded();
                OnRegisteringEvents();
            }
            catch
            {
                Log.Error($"Can't load plugin {assembly.GetName().Name}");
                OnUnregisteringEvents();
                throw;
            }
        }

        /// <summary>
        /// Метод, вызываемый для регистрации событий
        /// </summary>
        protected virtual void OnRegisteringEvents()
        {
            EventManager.RegisterAllEvents(Loader);
        }

        /// <summary>
        /// Метод, вызываемый для отмены регистрации событий
        /// </summary>
        protected virtual void OnUnregisteringEvents()
        {
            EventManager.UnregisterAllEvents(Loader);
        }

        /// <summary>
        /// Метод, вызываемый при инициализации плагина
        /// </summary>
        protected abstract void OnLoaded();
    }
}