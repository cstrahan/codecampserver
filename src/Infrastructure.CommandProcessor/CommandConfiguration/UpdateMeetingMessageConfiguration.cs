using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Services.BusinessRule.UpdateMeeting;
using CodeCampServer.UI.Models.Input;
using MvcContrib.CommandProcessor.Configuration;
using MvcContrib.CommandProcessor.Validation;
using MvcContrib.CommandProcessor.Validation.Rules;

namespace CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration
{
	public class UpdateMeetingMessageConfiguration : MessageDefinition<MeetingInput>
	{
		public UpdateMeetingMessageConfiguration()
		{
			Execute<UpdateMeetingCommandMessage>()
				.Enforce(v =>
				         	{
				         		v.Rule<ValidateDateRequired>(m => m.Meeting.StartDate).RefersTo(m => m.StartDate);
				         		v.Rule<ValidateDateRequired>(m => m.Meeting.EndDate).RefersTo(m => m.EndDate);
				         		//v.Rule<ValidateDateComesAfter>(m => m.Meeting.EndDate, m => m.Meeting.StartDate).RefersTo(m=>m.StartDate);
				         		v.Rule<KeyMustBeUnique>().RefersTo(m => m.Key);
				         	});
		}
	}

	public class KeyMustBeUnique : IValidationRule
	{
		private readonly IMeetingRepository _repository;

		public KeyMustBeUnique(IMeetingRepository repository)
		{
			_repository = repository;
		}

		public bool StopProcessing
		{
			get { return false; }
		}

		public string IsValid(object input)
		{
			return MeetingKeyAlreadyExists((UpdateMeetingCommandMessage) input) ? "Meeting key is already in use." : null;
		}

		private bool MeetingKeyAlreadyExists(UpdateMeetingCommandMessage message)
		{
			var entity = _repository.GetByKey(message.Meeting.Key);
			return entity != null && !Equals(entity.Id, message.Meeting.Id);
		}
	}
}