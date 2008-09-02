using System;
using CodeCampServer.Model;
using CodeCampServer.Model.Domain;
using CodeCampServer.Model.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Model
{
	[TestFixture]
	public class ConferenceServiceTester
	{
		private MockRepository _mocks;
		private IConferenceRepository _conferenceRepository;
		private IPersonRepository _personRepository;
		private IUserSession _userSession;
		private ICryptographer _cryptographer;

		[SetUp]
		public void Setup()
		{
			_mocks = new MockRepository();
			_conferenceRepository = _mocks.StrictMock<IConferenceRepository>();
			_personRepository = _mocks.StrictMock<IPersonRepository>();
			_userSession = _mocks.DynamicMock<IUserSession>();
			_cryptographer = _mocks.DynamicMock<ICryptographer>();
		}

		private IConferenceService getService(IClock clock)
		{
			return new ConferenceService(_conferenceRepository, _cryptographer, clock);
		}

		[Test]
		public void RegisterAttendeeShouldAddAttendeeToConferenceAndSaveConference()
		{
			var clockStub = new ClockStub(new DateTime(2008, 2, 15));
			IConferenceService service = getService(clockStub);
			var conference = _mocks.StrictMock<Conference>();

			Expect.Call(_cryptographer.CreateSalt()).Return(null);
			Expect.Call(() => conference.AddAttendee(null)).IgnoreArguments();
			Expect.Call(() => _conferenceRepository.Save(conference));
			_mocks.ReplayAll();

			Person person = service.RegisterAttendee("", "", "", "", "", conference, "");

			_mocks.VerifyAll();
		}

		[Test]
		public void CurrentConferenceShouldGetNextUpcomingConferenceIfThereIsOneOtherwiseMostRecent()
		{
			var oldConference = new Conference("old-conf", "old conference")
			                    	{StartDate = new DateTime(2007, 7, 1), PubliclyVisible = true};
			var nextConference = new Conference("new-conf", "new conference")
			                     	{StartDate = new DateTime(2008, 10, 1), PubliclyVisible = true};

			Expect.Call(_conferenceRepository.GetFirstConferenceAfterDate(new DateTime()))
				.IgnoreArguments()
				.Return(null);

			Expect.Call(_conferenceRepository.GetMostRecentConference(new DateTime()))
				.IgnoreArguments()
				.Return(oldConference);

			Expect.Call(_conferenceRepository.GetFirstConferenceAfterDate(new DateTime()))
				.IgnoreArguments()
				.Return(nextConference);

			_mocks.ReplayAll();

			var clockStub = new ClockStub(new DateTime(2008, 2, 15));
			IConferenceService service = getService(clockStub);

			//first one should not have a future conference
			Conference conf = service.GetCurrentConference();
			Assert.That(conf, Is.EqualTo(oldConference));

			//this one should have a future conference
			conf = service.GetCurrentConference();
			Assert.That(conf, Is.EqualTo(nextConference));

			_mocks.VerifyAll();
		}
	}
}