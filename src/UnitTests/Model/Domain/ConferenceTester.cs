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
			Sponsor sponsor = new Sponsor();
			Sponsor sponsor2 = new Sponsor();
			conference.AddSponsor(sponsor, SponsorLevel.Bronze);
			conference.AddSponsor(sponsor2, SponsorLevel.Gold);

			Sponsor[] sponsors = conference.GetSponsors();
			Assert.That(sponsors.Length, Is.EqualTo(2));
			Array.Sort(sponsors, delegate(Sponsor x, Sponsor y) { return x.Level.CompareTo(y.Level); });
			Assert.That(sponsors[0].Level, Is.EqualTo(SponsorLevel.Bronze));
			Assert.That(sponsors[1].Level, Is.EqualTo(SponsorLevel.Gold));
			Assert.That(sponsors[0], Is.EqualTo(sponsor));
			Assert.That(sponsors[1], Is.EqualTo(sponsor2));
		}

		[Test]
		public void ShouldProvideSponsorsSortedByLevelDescending()
		{
			Conference conference = new Conference();
			conference.AddSponsor(new Sponsor("test", "", ""), SponsorLevel.Silver);
			conference.AddSponsor(new Sponsor("test1", "", ""), SponsorLevel.Platinum);
			conference.AddSponsor(new Sponsor("test2", "", ""), SponsorLevel.Gold);

			Sponsor[] sponsors = conference.GetSponsors();

			Assert.That(sponsors[0].Level, Is.EqualTo(SponsorLevel.Platinum));
			Assert.That(sponsors[1].Level, Is.EqualTo(SponsorLevel.Gold));
			Assert.That(sponsors[2].Level, Is.EqualTo(SponsorLevel.Silver));
		}

		[Test]
		public void ShouldProvideSponsorsFilteredByLevel()
		{
			Conference conference = new Conference();
			conference.AddSponsor(new Sponsor("test", "", ""), SponsorLevel.Gold);
			conference.AddSponsor(new Sponsor("test1", "", ""), SponsorLevel.Platinum);
			conference.AddSponsor(new Sponsor("test2", "", ""), SponsorLevel.Platinum);
			conference.AddSponsor(new Sponsor("test2", "", ""), SponsorLevel.Platinum);

			Sponsor[] platinumSponsors = conference.GetSponsors(SponsorLevel.Platinum);

			Assert.That(platinumSponsors.Length, Is.EqualTo(2));
			Assert.That(platinumSponsors[0].Level, Is.EqualTo(SponsorLevel.Platinum));
			Assert.That(platinumSponsors[1].Level, Is.EqualTo(SponsorLevel.Platinum));
		}
	}
}