using CodeCampServer.Core.Services.BusinessRule.UpdateSponsor;
using CodeCampServer.UI.Models.Input;
using MvcContrib.CommandProcessor.Configuration;

namespace CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration
{
	public class UpdateSponsorMessageConfiguration : MessageDefinition<UpdateSponsorInput>
	{
		public UpdateSponsorMessageConfiguration()
		{
			Execute<UpdateSponsorCommandMessage>();
		}
	}
}