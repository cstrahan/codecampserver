using System;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.UI.Messages
{
	public class DeleteMeetingMessage : IMessage
	{
		public Guid Meeting { get; set; }
	}
}