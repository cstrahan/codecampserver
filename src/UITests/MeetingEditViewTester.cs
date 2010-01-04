using System;
using CodeCampServer.DependencyResolution;
using CodeCampServer.UI.InputBuilders;
using CodeCampServer.UI.Models.Input;
using NUnit.Framework;
using StructureMap;
using UITestHelper;
using WatiN.Core;

namespace CodeCampServerUiTests
{
	[TestFixture, Ignore]
	public class MeetingEditViewTester
	{
		private IE _ie;

		[SetUp]
		public void Setup()
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			var baseurl = System.Configuration.ConfigurationManager.AppSettings["url"];
			_ie = new IE(baseurl + "/Meeting/New");

			new UITestRegistry();
//			InputBuilderPropertyFactory.CreateDependencyCallback = (t) => ObjectFactory.GetInstance(t);

		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			throw new NotImplementedException();
		}

		[TearDown]
		public void teardown()
		{
			_ie.Dispose();
		}

		public FluentForm<T> Form<T>()
		{
			return new FluentForm<T>(new WatinDriver(_ie));
		}

		[Test]
		public void Should_create_a_new_meeting()
		{
			Form<MeetingInput>()
				.WithInput(m => m.Name, "TX")
				.WithInput(m => m.Topic, "my topic")
				.WithInput(m => m.Summary, "this will be a normal meeting")
				.WithInput(m => m.Description, "The description")
				.WithInput(m => m.Key, "foe")
				.WithInput(m => m.LocationName, "our location")
				.WithInput(m => m.LocationUrl, "http://foolocation.com")
				.WithInput(m => m.SpeakerBio, "this is a great speaker")
				.WithInput(m => m.SpeakerName, "bart simpson")
				.WithInput(m => m.SpeakerUrl, "http://thesimpsons.com")
				.WithInput(m => m.TimeZone, "CST")
				.WithInput(m => m.EndDate, new DateTime(2009, 12, 12))
				.WithInput(m => m.StartDate, "12/12/2009")
				.Submit("submitForm");
		}
	}
}