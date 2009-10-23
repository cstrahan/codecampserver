using System;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.UI.Models.Input
{
	public class DeleteUserGroupInput:IMessage
	{
		public Guid UserGroup { get; set; }
	}
}