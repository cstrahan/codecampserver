using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;
using CodeCampServer.Core.Services;
using CodeCampServer.Core.Services.Impl;
using CodeCampServer.DependencyResolution;
using CodeCampServer.IntegrationTests.Infrastructure.DataAccess;
using NUnit.Framework;
using StructureMap;

namespace CodeCampServer.IntegrationTests.UI.DataLoader
{
	[TestFixture]
	public class ZDataLoader : DataTestBase
	{
		[Test, Category("DataLoader")]
		public void DataLoader()
		{
//			Logger.EnsureInitialized();
			DependencyRegistrar.EnsureDependenciesRegistered();
			ObjectFactory.Inject(typeof (IUserSession), new UserSessionStub(null));
			LoadData();
		}

		private void LoadData()
		{
			var userGroup = new UserGroup
			                	{
			                		Name = "Austin .Net Users Group",
			                		City = "Austin",
			                		Region = "Texas",
			                		Country = "USA",
			                		Key = "default",
			                		HomepageHTML = "Austin .Net Users Group",
			                	};
			userGroup.Add(new Sponsor
			              	{
			              		Level = SponsorLevel.Platinum,
			              		Name = "Microsoft",
			              		Url = "http://microsoft.com/",
			              		BannerUrl = "http://www.microsoft.com/presspass/images/gallery/logos/web/net_v_web.jpg"
			              	});
			var conference = new Conference
			                 	{
			                 		Address = "123 Guadalupe Street",
			                 		City = "Austin",
			                 		Description = "Texas' Premier Software Community Event",
			                 		EndDate = new DateTime(2009, 5, 10),
			                 		StartDate = new DateTime(2009, 5, 9),
			                 		Key = "austincodecamp",
			                 		LocationName = "St. Edward's Professional Education Center",
			                 		Name = "Austin Code Camp",
			                 		PhoneNumber = "(512) 555-1212",
			                 		PostalCode = "78787",
			                 		Region = "Texas",
			                 		UserGroup = userGroup,
			                 		HtmlContent =
			                 			@"
                                    <p>
<script type=""text/javascript"" src=""http://feeds2.feedburner.com/AustinCodeCamp?format=sigpro""></script>
</p>
<noscript></noscript>
<p><iframe marginwidth=""0"" marginheight=""0"" 
src=""http://maps.google.com/maps?f=q&amp;source=s_q&amp;hl=en&amp;geocode=&amp;q=9420+Research+Blvd,+Austin,+TX+78759+(St+Edward's+PEC)&amp;sll=30.384022,-97.743998&amp;sspn=0.006858,0.013626&amp;ie=UTF8&amp;ll=30.397013,-97.74004&amp;spn=0.025911,0.036478&amp;z=14&amp;iwloc=addr&amp;output=embed"" 
frameborder=""0"" width=""425"" scrolling=""no"" height=""350""></iframe><br /><small>
<a style=""color:#0000FF;text-align:left"" 
href=""http://maps.google.com/maps?f=q&amp;source=embed&amp;hl=en&amp;geocode=&amp;q=9420+Research+Blvd,+Austin,+TX+78759+(St+Edward's+PEC)&amp;sll=30.384022,-97.743998&amp;sspn=0.006858,0.013626&amp;ie=UTF8&amp;ll=30.397013,-97.74004&amp;spn=0.025911,0.036478&amp;z=14&amp;iwloc=addr"">
View Larger Map</a></small></p>"
			                 	};


			var list = new List<PersistentObject>();
			User[] users = CreateUsers();
			list.AddRange(users);
			userGroup.Add(users[0]);
			list.Add(userGroup.GetSponsors()[0]);
			list.Add(userGroup);
			list.Add(conference);

			IEnumerable<Conference> conferences = CreateConferences(userGroup);
			IEnumerable<Meeting> meetings = CreateMeetings(userGroup);
			list.AddRange(conferences.ToArray());
			list.AddRange(meetings.ToArray());


			PersistEntities(list.ToArray());
		}

		private static IEnumerable<Conference> CreateConferences(UserGroup userGroup)
		{
			DateTime startDate = DateTime.Now.AddDays(-7*5).AddMinutes(1);
			for (int i = 0; i < 6; i++)
			{
				DateTime conferenceDate = startDate.AddDays(7*i);
				yield return new Conference
				             	{
				             		Address = "123 Guadalupe Street",
				             		City = "Austin",
				             		Description = "Community Event",
				             		EndDate = conferenceDate.AddDays(1),
				             		StartDate = conferenceDate,
				             		Key = "conference" + i,
				             		LocationName = "St. Edward's Professional Education Center",
				             		Name = "Conference " + i,
				             		PhoneNumber = "(512) 555-1212",
				             		PostalCode = "78787",
				             		Region = "Texas",
				             		UserGroup = userGroup
				             	};
			}
		}

		private static IEnumerable<Meeting> CreateMeetings(UserGroup userGroup)
		{
			DateTime startDate = DateTime.Now.AddDays(-7*5);
			for (int i = 0; i < 6; i++)
			{
				DateTime meetingDate = startDate.AddDays(7*i);
				yield return new Meeting
				             	{
				             		Address = "123 Guadalupe Street",
				             		City = "Austin",
				             		Description = "Regular meeting.  Don't forget CodeCamp planning next month!",
				             		EndDate = meetingDate.AddDays(1),
				             		StartDate = meetingDate,
				             		Key = meetingDate.Month.ToString().ToLower() + meetingDate.Day + "meeting",
				             		LocationName = "St. Edward's Professional Education Center",
				             		Name = meetingDate.ToString("MMMM") + " meeting",
				             		PostalCode = "78787",
				             		Region = "Texas",
				             		UserGroup = userGroup,
				             		Topic = "ASP.NET MVC in Action",
				             		Summary =
				             			"With the new version of ASP.NET, developers can easily leverage the Model-View-Controller pattern in ASP.NET applications. Pulling logic away from the UI and the views has been difficult for a long time. The Model-View-Presenter pattern helps a little bit, but the fact that the view has to delegate to the presenter makes the UI pattern difficult to work with. This session is a detailed overview of the ASP.NET MVC Framework.  It is meant for developers already building systems with ASP.NET 3.5 SP1.",
				             		LocationUrl = "http://maps.google.com",
				             		TimeZone = "CST",
				             		SpeakerName = "Jeffrey Palermo",
				             		SpeakerBio =
				             			@"Jeffrey Palermo is the CTO of Headspring Systems. Jeffrey specializes in Agile management coaching and helps companies double the productivity of software teams. He is instrumental in the Austin software community as a member of AgileAustin and a director of the Austin .Net User Group. Jeffrey has been recognized by Microsoft as a ""Microsoft Most Valuable Professional"" (MVP) for technical and community leadership. He is also certified as a MCSD.Net and ScrumMaster. Jeffrey has spoken and facilitated at industry conferences such as VSLive, DevTeach, and Microsoft Tech Ed. He also speaks to user groups around the country as part of the INETA Speakers' Bureau. His web sites are headspringsystems.com and jeffreypalermo.com. He is a graduate from Texas A&amp;M University, an Eagle Scout, and an Iraq war veteran.  Jeffrey is the founder of the CodeCampServer open-source project and a co-founder of the MvcContrib project.",
				             		SpeakerUrl = "http://jeffreypalermo.com"
				             	};
			}
		}

		private User[] CreateUsers()
		{
			var crypto = new Cryptographer();
			string salt = crypto.CreateSalt();
			return new[]
			       	{
			       		new User
			       			{
			       				Name = "Joe User",
			       				Username = "admin",
			       				EmailAddress = "joe@user.com",
			       				PasswordHash = crypto.GetPasswordHash("password", salt),
			       				PasswordSalt = salt,
			       			},
			       		new User
			       			{
			       				Name = "Jeffrey Palermo",
			       				EmailAddress = "jeffis@theman.com",
			       				Username = "jpalermo",
			       				PasswordHash = crypto.GetPasswordHash("beer", salt),
			       				PasswordSalt = salt,
			       			},
			       		new User
			       			{
			       				Name = "Homer Simpson",
			       				EmailAddress = "homer@simpsons.com",
			       				Username = "hsimpson",
			       				PasswordHash = crypto.GetPasswordHash("beer", salt),
			       				PasswordSalt = salt,
			       			},
			       		new User
			       			{
			       				Name = "Bart Simpson",
			       				EmailAddress = "bart@simpsons.com",
			       				Username = "bsimpson",
			       				PasswordHash = crypto.GetPasswordHash("beer", salt),
			       				PasswordSalt = salt,
			       			}
			       	};
		}
	}
}