using System;
using CodeCampServer.Model.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace CodeCampServer.UnitTests.Model.Domain
{
	[TestFixture]
	public class ConferenceTester : EntityTesterBase
	{
		protected override EntityBase createEntity()
		{
			return new Conference();
		}

		[Test]
		public void ShouldAcceptSponsors()
		{
            Conference conference = new Conference();
            Sponsor sponsor = new Sponsor("test", "", "", "", "", "", SponsorLevel.Bronze);
            Sponsor sponsor2 = new Sponsor("test1", "", "", "", "", "", SponsorLevel.Gold);
            conference.AddSponsor(sponsor);
            conference.AddSponsor(sponsor2);

			Sponsor[] sponsors = conference.GetSponsors();
			Assert.That(sponsors.Length, Is.EqualTo(2));
			Array.Sort(sponsors, delegate(Sponsor x, Sponsor y) { return x.Level.CompareTo(y.Level); });
			Assert.That(sponsors[0].Level, Is.EqualTo(SponsorLevel.Bronze));
			Assert.That(sponsors[1].Level, Is.EqualTo(SponsorLevel.Gold));
			Assert.That(sponsors[0], Is.EqualTo(sponsor));
			Assert.That(sponsors[1], Is.EqualTo(sponsor2));
		}

        [Test]
        public void ShouldNotAcceptDuplicateSponsors()
        {
            Conference conference = new Conference();
            Sponsor sponsor = new Sponsor("test", "", "", "", "", "", SponsorLevel.Bronze);
            conference.AddSponsor(sponsor);
            conference.AddSponsor(sponsor);
            Sponsor[] sponsors = conference.GetSponsors();
            Assert.That(sponsors.Length, Is.EqualTo(1));
        }

        [Test]
        public void ShouldProvideSponsorsSortedByLevelDescending()
        {
            Conference conference = new Conference();
            conference.AddSponsor(new Sponsor("test", "", "", "", "", "", SponsorLevel.Silver));
            conference.AddSponsor(new Sponsor("test1", "", "", "", "", "", SponsorLevel.Platinum));
            conference.AddSponsor(new Sponsor("test2", "", "", "", "", "", SponsorLevel.Gold));

            Sponsor[] sponsors = conference.GetSponsors();

            Assert.That(sponsors[0].Level, Is.EqualTo(SponsorLevel.Platinum));
            Assert.That(sponsors[1].Level, Is.EqualTo(SponsorLevel.Gold));
            Assert.That(sponsors[2].Level, Is.EqualTo(SponsorLevel.Silver));
        }

        [Test]
        public void ShouldProvideSponsorsFilteredByLevel()
        {
            Conference conference = new Conference();
            conference.AddSponsor(new Sponsor("test", "", "", "", "", "", SponsorLevel.Gold));
            conference.AddSponsor(new Sponsor("test1", "", "", "", "", "", SponsorLevel.Platinum));
            conference.AddSponsor(new Sponsor("test2", "", "", "", "", "", SponsorLevel.Platinum));
            conference.AddSponsor(new Sponsor("test3", "", "", "", "", "", SponsorLevel.Bronze));

            Sponsor[] platinumSponsors = conference.GetSponsors(SponsorLevel.Platinum);

            Assert.That(platinumSponsors.Length, Is.EqualTo(2));
            Assert.That(platinumSponsors[0].Level, Is.EqualTo(SponsorLevel.Platinum));
            Assert.That(platinumSponsors[1].Level, Is.EqualTo(SponsorLevel.Platinum));
        }

        [Test]
        public void ShouldGetSponsorByName()
        {
            Conference conference = new Conference();
            Sponsor sponsorToReturn = new Sponsor("test", "", "", "", "", "", SponsorLevel.Gold);
            conference.AddSponsor(sponsorToReturn);
            Sponsor sponsor = conference.GetSponsor("test");
            Sponsor sameSponsor = conference.GetSponsor("TesT");
            Assert.That(sponsor, Is.EqualTo(sponsorToReturn));
            Assert.That(sponsor.Level, Is.EqualTo(SponsorLevel.Gold));
            Assert.That(sponsor, Is.EqualTo(sameSponsor));
        }

        [Test]
        public void ShouldRemoveSponsor()
        {
            Conference conference = new Conference();
            Sponsor test = new Sponsor("test", "", "", "", "", "", SponsorLevel.Gold);
            Sponsor test1 = new Sponsor("test1", "", "", "", "", "", SponsorLevel.Gold);
            conference.AddSponsor(test);
            conference.AddSponsor(test1);

            conference.RemoveSponsor(test);

            Sponsor[] sponsors = conference.GetSponsors();

            Assert.That(sponsors.Length, Is.EqualTo(1));
            Assert.That(sponsors[0].Name, Is.EqualTo("test1"));
        }
	}
}
