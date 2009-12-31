using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using Tarantino.RulesEngine;
using Tarantino.RulesEngine.CommandProcessor;

namespace CodeCampServer.Core.Services.BusinessRule.UpdateUserGroup
{
	public class UpdateSponsorCommandHandler : Command<UpdateSponsorCommandMessage>
	{
		private readonly ISponsorRepository _repository;

		public UpdateSponsorCommandHandler(ISponsorRepository repository)
		{
			_repository = repository;
		}

		protected override ReturnValue Execute(UpdateSponsorCommandMessage commandMessage)
		{
			Sponsor sponsor = commandMessage.Id ?? new Sponsor(){UserGroup = commandMessage.UserGroup};
			sponsor.Level = commandMessage.Level;
			sponsor.BannerUrl = commandMessage.BannerUrl;
			sponsor.Name = commandMessage.Name;
			sponsor.Url = commandMessage.Url;

			_repository.Save(sponsor);
			return new ReturnValue {Type = typeof (Sponsor), Value = sponsor};
		}
	}
}