using System.Runtime.Serialization;

namespace Padoru.Logger.Structs
{
	public class WebhookParams
	{
		[DataMember(Name = "content")]
		public string Content;

		public WebhookParams(string content)
		{
			Content = content;
		}
	}
}