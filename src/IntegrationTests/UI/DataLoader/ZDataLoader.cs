using System;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Domain.Model.Enumerations;
using CodeCampServer.DependencyResolution;
using CodeCampServer.IntegrationTests.Infrastructure.DataAccess;
using NUnit.Framework;

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
			DeleteAllObjects();
			LoadData();
		}

		private void LoadData()
		{
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
			                 		Region = "Texas"
			                 	};

			var track = new Track {Conference = conference, Name = "ALT.NET"};
			var track1 = new Track {Conference = conference, Name = "Web"};
			var track2 = new Track {Conference = conference, Name = "Project Management"};
			var track3 = new Track {Conference = conference, Name = "General Development"};

			var timeslot = new TimeSlot
			               	{
			               		Conference = conference,
			               		StartTime = new DateTime(2009, 5, 9, 9, 0, 0),
			               		EndTime = new DateTime(2009, 5, 10, 10, 30, 0)
			               	};
			var timeslot1 = new TimeSlot
			                	{
			                		Conference = conference,
			                		StartTime = new DateTime(2009, 5, 9, 11, 0, 0),
			                		EndTime = new DateTime(2009, 5, 10, 12, 30, 0)
			                	};
			var timeslot2 = new TimeSlot
			                	{
			                		Conference = conference,
			                		StartTime = new DateTime(2009, 5, 9, 14, 0, 0),
			                		EndTime = new DateTime(2009, 5, 10, 15, 30, 0)
			                	};
			var timeslot3 = new TimeSlot
			                	{
			                		Conference = conference,
			                		StartTime = new DateTime(2009, 5, 9, 16, 0, 0),
			                		EndTime = new DateTime(2009, 5, 10, 17, 30, 0)
			                	};

			var timeslot4 = new TimeSlot
			                	{
			                		Conference = conference,
			                		StartTime = new DateTime(2009, 5, 10, 9, 0, 0),
			                		EndTime = new DateTime(2009, 5, 10, 10, 30, 0)
			                	};
			var timeslot5 = new TimeSlot
			                	{
			                		Conference = conference,
			                		StartTime = new DateTime(2009, 5, 10, 11, 0, 0),
			                		EndTime = new DateTime(2009, 5, 10, 12, 30, 0)
			                	};
			var timeslot6 = new TimeSlot
			                	{
			                		Conference = conference,
			                		StartTime = new DateTime(2009, 5, 10, 14, 0, 0),
			                		EndTime = new DateTime(2009, 5, 10, 15, 30, 0)
			                	};
			var timeslot7 = new TimeSlot
			                	{
			                		Conference = conference,
			                		StartTime = new DateTime(2009, 5, 10, 16, 0, 0),
			                		EndTime = new DateTime(2009, 5, 10, 17, 30, 0)
			                	};


			var speaker = new Speaker
			              	{
			              		Bio = "Web Wizard",
			              		Company = "Software Inc.",
			              		EmailAddress = "wizard@example.com",
			              		FirstName = "Joe",
			              		LastName = "Httpson",
			              		JobTitle = "Sr. Developer",
			              		Key = "httpson",
			              		WebsiteUrl = "http://www.example.com/wizard"
			              	};

			var speaker2 = new Speaker
			               	{
			               		Bio = "Big Executive",
			               		Company = "Micro Tech",
			               		EmailAddress = "ceo@example.com",
			               		FirstName = "Steven",
			               		LastName = "Bossman",
			               		JobTitle = "CEO",
			               		Key = "bossman",
			               		WebsiteUrl = "http://www.example.com/bossman"
			               	};

			var speaker3 = new Speaker
			               	{
			               		Bio = "Big Time Geek",
			               		Company = "Planet Soft",
			               		EmailAddress = "foo@example.com",
			               		FirstName = "Susan",
			               		LastName = "Bytecode",
			               		JobTitle = "Software Development Manager",
			               		Key = "Susanbytecode",
			               		WebsiteUrl = "http://www.example.com/geek"
			               	};

			var speaker4 = new Speaker
			               	{
			               		Bio = "Programming Genius From Canada",
			               		Company = "ColdCode",
			               		EmailAddress = "eh@example.com",
			               		FirstName = "Peter",
			               		LastName = "Keyboard",
			               		JobTitle = "Techncial Fellow",
			               		Key = "coldcoder",
			               		WebsiteUrl = "http://www.example.com/coldcoder"
			               	};

			var speaker5 = new Speaker
			               	{
			               		Bio = "Book Writer",
			               		Company = "",
			               		EmailAddress = "bookwork@example.com",
			               		FirstName = "David",
			               		LastName = "Writesalot",
			               		JobTitle = "Writer",
			               		Key = "davidw",
			               		WebsiteUrl = "http://www.example.com/buymybook"
			               	};

			var speaker6 = new Speaker
			               	{
			               		Bio = "Open Source Hacker",
			               		Company = "",
			               		EmailAddress = "oss@example.com",
			               		FirstName = "Mark",
			               		LastName = "Sendmepatch",
			               		JobTitle = "",
			               		Key = "sendmepatch",
			               		WebsiteUrl = "http://www.example.com/sendmepatch"
			               	};

			var speaker7 = new Speaker
			               	{
			               		Bio = "Web Design Guru",
			               		Company = "DeZinEs",
			               		EmailAddress = "linda@example.com",
			               		FirstName = "Linda",
			               		LastName = "Ihatetables",
			               		JobTitle = "Web Designer",
			               		Key = "tablehater",
			               		WebsiteUrl = "http://www.example.com/linda"
			               	};



			var session = new Session()
			              	{
			              		Abstract = "REST information and code samples",
			              		Conference = conference,
			              		Key = "webrest",
			              		Level = SessionLevel.L200,
			              		MaterialsUrl = "http://example.com/rest",


			              	};


			PersistEntities(conference, track, track1, track2, track3, timeslot, timeslot1, timeslot2, timeslot3, timeslot4,
			                timeslot5, timeslot6, timeslot7, speaker, speaker2, speaker3, speaker4, speaker5, speaker6, speaker7);
		}
	}
}