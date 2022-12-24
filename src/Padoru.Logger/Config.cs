using System.ComponentModel;
using Padoru.API.Features.Plugins;
using Padoru.Logger.Structs;

namespace Padoru.Logger
{
	public class Config : IConfig
	{
		[Description("Включён ли плагин или нет")]
		public bool IsEnabled { get; set; } = false;
		
		[Description("Токен вебхука")]
		public string WebhookUrl { get; set; } = string.Empty;

		[Description("События который будут логироваться")]
		public LoggingEvents LoggingEvents { get; set; } = new();
	}
}