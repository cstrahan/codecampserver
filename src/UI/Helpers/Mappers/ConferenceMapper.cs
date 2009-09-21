using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Forms;

namespace CodeCampServer.UI.Helpers.Mappers
{
	public class ConferenceMapper : EventMapperBase<Conference, ConferenceForm>, IConferenceMapper
	{
		public ConferenceMapper(IConferenceRepository repository, IUserGroupRepository userGroupRepository)
			: base(repository, userGroupRepository) {}


		protected override void MapToModel(ConferenceForm form, Conference model)
		{
			base.MapToModel(form, model);
			model.PhoneNumber = form.PhoneNumber;
			model.HtmlContent = form.HtmlContent;
			model.HasRegistration = form.HasRegistration;
		}
	}
}