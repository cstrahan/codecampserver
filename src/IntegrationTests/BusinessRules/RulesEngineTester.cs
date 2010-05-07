using System;
using System.Collections.Generic;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;
using CodeCampServer.Core.Services;
using CodeCampServer.DependencyResolution;
using CodeCampServer.Infrastructure.Automapper.ObjectMapping;
using CodeCampServer.Infrastructure.CommandProcessor;
using CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration;
using CodeCampServer.UI.Models.Input;
using CodeCampServer.UnitTests;
using MvcContrib.CommandProcessor.Interfaces;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap;
using RulesEngine=MvcContrib.CommandProcessor.RulesEngine;

namespace CodeCampServer.IntegrationTests.BusinessRules
{
	[TestFixture]
	public class RulesEngineTester : TestBase
	{
		[SetUp]
		public void Setup()
		{
			ObjectFactory.ResetDefaults();
			ObjectFactory.AssertConfigurationIsValid();
		}

		[Test]
		public void DeleteMeeting_message_should_delete_a_meeting()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			AutoMapperConfiguration.Configure();
			ObjectFactory.Inject(typeof (IUnitOfWork), S<IUnitOfWork>());

			var repository = S<IRepository<Meeting>>();
			var meeting = new Meeting();
			repository.Stub(repository1 => repository1.GetById(Guid.Empty)).IgnoreArguments().Return(meeting);
			ObjectFactory.Inject(typeof (IRepository<Meeting>), repository);
			ObjectFactory.Inject(typeof (IMeetingRepository), S<IMeetingRepository>());

			RulesEngineConfiguration.Configure(typeof (UpdateUserConfiguration));
			var rulesRunner = new RulesEngine();

			var result = rulesRunner.Process(new DeleteMeetingInput {Meeting = Guid.NewGuid()},
			                                 typeof (DeleteMeetingInput));
			result.Successful.ShouldBeTrue();
			result.ReturnItems.Get<Meeting>().ShouldEqual(meeting);
		}

		[Test]
		public void UpdateMeeting_should_save_a_meeting()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			AutoMapperConfiguration.Configure();
			ObjectFactory.Inject(typeof (IUnitOfWork), S<IUnitOfWork>());

			var repository = S<IRepository<Meeting>>();
			ObjectFactory.Inject(typeof (IRepository<Meeting>), repository);
			var meetingRepository = S<IMeetingRepository>();
			ObjectFactory.Inject(typeof (IMeetingRepository), meetingRepository);

			var userGroupRepository = S<IUserGroupRepository>();
			userGroupRepository.Stub(groupRepository => groupRepository.GetById(Guid.Empty)).Return(new UserGroup());
			ObjectFactory.Inject(typeof (IUserGroupRepository), userGroupRepository);

			RulesEngineConfiguration.Configure(typeof (UpdateMeetingMessageConfiguration));
			var rulesRunner = new RulesEngine();

			var result =
				rulesRunner.Process(
					new MeetingInput {Description = "New Meeting", StartDate = DateTime.Now, EndDate = DateTime.Now},
					typeof (MeetingInput));
			result.Successful.ShouldBeTrue();
			result.ReturnItems.Get<Meeting>().ShouldNotBeNull();

			meetingRepository.AssertWasCalled(r => r.Save(null), options => options.IgnoreArguments());
		}

		[Test]
		public void Update_UserGroup_should_save_a_usergroup()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			AutoMapperConfiguration.Configure();
			ObjectFactory.Inject(typeof (IUnitOfWork), S<IUnitOfWork>());

			var repository = S<IRepository<UserGroup>>();
			ObjectFactory.Inject(typeof (IRepository<UserGroup>), repository);

			var userGroupRepository = S<IUserGroupRepository>();
			ObjectFactory.Inject(typeof (IUserGroupRepository), userGroupRepository);

			var userRepository = S<IUserRepository>();
			ObjectFactory.Inject(typeof (IUserRepository), userRepository);

			RulesEngineConfiguration.Configure(typeof (UpdateUserGroupMessageConfiguration));
			var rulesRunner = new RulesEngine();

			var result = rulesRunner.Process(new UserGroupInput
			                                 	{
			                                 		Name = "New Meeting",
			                                 		Users = new List<UserSelectorInput>(),
			                                 		Sponsors = new List<UpdateSponsorInput>(),
			                                 	}, typeof (UserGroupInput));
			result.Successful.ShouldBeTrue();
			result.ReturnItems.Get<UserGroup>().ShouldNotBeNull();

			userGroupRepository.AssertWasCalled(r => r.Save(null), options => options.IgnoreArguments());
		}

		[Test]
		public void Delete_user_group_should_do_just_that()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			AutoMapperConfiguration.Configure();
			ObjectFactory.Inject(typeof (IUnitOfWork), S<IUnitOfWork>());


			var repository = S<IUserGroupRepository>();
			var repositoryT = S<IRepository<UserGroup>>();
			repositoryT.Stub(repository1 => repository1.GetById(Guid.Empty)).Return(new UserGroup());
			ObjectFactory.Inject(typeof (IRepository<UserGroup>), repositoryT);
			ObjectFactory.Inject(typeof (IUserGroupRepository), repository);

			var userRepository = S<IUserRepository>();
			ObjectFactory.Inject(typeof (IUserRepository), userRepository);


			RulesEngineConfiguration.Configure(typeof (UpdateUserConfiguration));

			var rulesRunner = new RulesEngine();

			var result = rulesRunner.Process(new DeleteUserGroupInput
			                                 	{
			                                 		UserGroup = Guid.Empty,
			                                 	}, typeof (DeleteUserGroupInput));

			result.Successful.ShouldBeTrue();
			result.ReturnItems.Get<UserGroup>().ShouldNotBeNull();

			repository.AssertWasCalled(r => r.Delete(null), options => options.IgnoreArguments());
		}

		[Test]
		public void Update_User_should_save()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			AutoMapperConfiguration.Configure();
			ObjectFactory.Inject(typeof (IUnitOfWork), S<IUnitOfWork>());

			var repository = S<IRepository<User>>();
			ObjectFactory.Inject(typeof (IRepository<User>), repository);

			var userRepository = S<IUserRepository>();
			ObjectFactory.Inject(typeof (IUserRepository), userRepository);

			var cryp = S<ICryptographer>();
			ObjectFactory.Inject(typeof (ICryptographer), cryp);

			RulesEngineConfiguration.Configure(typeof (UpdateUserGroupMessageConfiguration));

			var rulesRunner = new RulesEngine();

			var result = rulesRunner.Process(new UserInput
			                                 	{
			                                 		Name = "New Meeting",
			                                 		Username = "foo",
			                                 		Password = "thepass",
			                                 		ConfirmPassword = "thepass",
			                                 		EmailAddress = "ere@sdfdsf.com",
			                                 		Id = Guid.Empty,
			                                 	}, typeof (UserInput));
			result.Successful.ShouldBeTrue();
			result.ReturnItems.Get<User>().ShouldNotBeNull();

			userRepository.AssertWasCalled(r => r.Save(null), options => options.IgnoreArguments());
		}

		[Test]
		public void Login_user()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			AutoMapperConfiguration.Configure();
			ObjectFactory.Inject(typeof (IUnitOfWork), S<IUnitOfWork>());

			var repository = S<IUserRepository>();
			ObjectFactory.Inject(typeof (IUserRepository), repository);

			var auth = S<IAuthenticationService>();
			ObjectFactory.Inject(typeof (IAuthenticationService), auth);
			auth.Stub(service => service.PasswordMatches(null, "")).IgnoreArguments().Return(true);

			repository.Stub(groupRepository => groupRepository.GetByUserName("foo")).Return(new User());
			RulesEngineConfiguration.Configure(typeof (UpdateUserGroupMessageConfiguration));

			var rulesRunner = new RulesEngine();

			var result = rulesRunner.Process(new LoginProxyInput
			                                 	{
			                                 		Username = "foo",
			                                 		Password = "thepass",
			                                 	}, typeof (LoginProxyInput));
			result.Successful.ShouldBeTrue();
			result.ReturnItems.Get<User>().ShouldNotBeNull();
		}

		[Test]
		public void Update_sponsor_should_save_a_usergroup()
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
			AutoMapperConfiguration.Configure();
			ObjectFactory.Inject(typeof (IUnitOfWork), S<IUnitOfWork>());

			var repository = S<IRepository<UserGroup>>();
			ObjectFactory.Inject(typeof (IRepository<UserGroup>), repository);
			repository.Stub(repository1 => repository1.GetById(Guid.Empty)).IgnoreArguments().Repeat.Any().Return(new UserGroup());

			var sponsorRepository = S<ISponsorRepository>();
			ObjectFactory.Inject(typeof (ISponsorRepository), sponsorRepository);
			sponsorRepository.Stub(repository1 => repository1.GetById(Guid.Empty)).IgnoreArguments().Repeat.Any().Return(
				new Sponsor());

			//var userRepository = S<IUserRepository>();
			///ObjectFactory.Inject(typeof(IUserRepository), userRepository);

			RulesEngineConfiguration.Configure(typeof (UpdateUserGroupMessageConfiguration));
			var rulesRunner = new RulesEngine();

			var result = rulesRunner.Process(new UpdateSponsorInput
			                                 	{
			                                 		Name = "New Meeting",
			                                 		BannerUrl = "the url",
			                                 		Id = 0,
			                                 		Level = SponsorLevel.Gold,
			                                 		Url = "http://foo.com",
			                                 	}, typeof (UpdateSponsorInput));

			result.Successful.ShouldBeTrue();
			result.ReturnItems.Get<Sponsor>().ShouldNotBeNull();

			sponsorRepository.AssertWasCalled(r => r.Save(null), options => options.IgnoreArguments());
		}
	}
}