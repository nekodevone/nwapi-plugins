using System.ComponentModel;
using Padoru.API.Features.Plugins;

namespace Padoru.Logger
{
	public class Config : IConfig
	{
		[Description("Включён ли плагин или нет")]
		public bool IsEnabled { get; set; } = false;
		
		[Description("Токен вебхука")]
		public string WebhookUrl { get; set; } = string.Empty;
	}
}