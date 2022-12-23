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
        /// Конфигурация плагина
        /// </summary>
        public TConfig Config { get; private set; }

        /// <summary>
        /// Метод, вызываемый из загрузчика для инициализации плагина
        /// </summary>
        public void Load(LoaderBase<TConfig> loaderBase)
        {
            var assembly = Assembly.GetCallingAssembly();

            Assembly = assembly;
            Config = loaderBase.Config;

            foreach (var type in assembly.FindInterfaces(typeof(IEventHandler)))
            {
                EventManager.RegisterEvents(loaderBase, type);
            }

            try
            {
                OnLoaded();
            }
            catch
            {
                Log.Error($"Can't load plugin {assembly.GetName().Name}");
                throw;
            }
        }

        /// <summary>
        /// Метод, вызываемый при инициализации плагина
        /// </summary>
        public abstract void OnLoaded();
    }
}