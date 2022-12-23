using System.ComponentModel;
using Padoru.API.Features.Plugins;

namespace Padoru.Logger
{
	public class Config : IConfig
	{
		[Description("Токен вебхука")]
		public string WebhookUrl { get; set; } = string.Empty;
	}
}