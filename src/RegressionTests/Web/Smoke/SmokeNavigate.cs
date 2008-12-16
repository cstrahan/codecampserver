using System.Threading;
using MbUnit.Framework;
using RegressionTests.TestHelpers.SmartWatiN;

namespace RegressionTests.Web.Smoke
{
	[TestFixture, Category("WatiN"), Category("Smoke"),Ignore()]
	[Description("Smoke Test - Verifies the user can navigate to each " +
	             "page in the application without any stack trace or NAAK errors.")]
	public class SmokeNavigate : WebTest
	{
		[Test]
		[Parallelizable]
		[ApartmentState(ApartmentState.STA)]
		[Description("Verifies user can get to the facilities list page")]
		public void CanGetToFacilityPage()
		{
			Follow(FACILITY_LIST, Until(FACILITY_LIST));
			Follow(FACILITY);
		}

		[Test]
		[Parallelizable]
		[ApartmentState(ApartmentState.STA)]
		[Description("Verifies user can get to feedback page")]
		public void CanGetToFeedbackPage()
		{
			Follow(FEEDBACK);
		}

		[Test]
		[Parallelizable]
		[ApartmentState(ApartmentState.STA)]
		[Row("1001"), Description("Verifies user can navigate through Juvenile's pages (Juvenile ID 1001")]
		public void CanNavigateThroughJuvenilePages(string id)
		{
			LogoutIfLoggedIn();
			LoginAs(UserName, Password);
			NavigateJuvenile(id);
		}

		[Test]
		[Parallelizable]
		[ApartmentState(ApartmentState.STA)]
		[Description("Verifies user can navigate through Juvenile's pages (Juvenile ID 1006")]
		public void CanNavigateThroughJuvenilePagesFor1006()
		{
			CanNavigateThroughJuvenilePages("1006");
		}

		private void NavigateJuvenile(string jcmsNumber)
		{
			Follow(SEARCH, With(new {number = jcmsNumber, idType = JCMS_NUMBER}), Until(JUVENILE_IDENTIFICATION));
			NavigateThroughLinksInJuvenilePageSubNavBar();
			NavigateThroughOffensesArrestsSection();
			NavigateThroughSchoolsSection();
			NavigateThroughReferralsSection();
			NavigateThroughDetentionSection();
			NavigateThroughProgramsServicesSection();
			NavigateThroughIntakeSection();
			NavigateThroughPlacementSection();
			NavigateThroughVictimServicesSection();
			NavigateThroughSupervisionSection();
			NavigateThroughCSRSection();
			NavigateThroughChronologicalNotesSection();
			NavigateThroughAssessmentHistorySection();
			NavigateThroughSubstanceAbuseSection();
			NavigateThroughCaseManagementSection();
			NavigateThroughBehavioralHealthSection();
		}

		// Behavioral Health
		private void NavigateThroughBehavioralHealthSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Behavioral Health section"))
			{
				Follow(BEHAVIORAL_HEALTH, Until(BEHAVIORAL_HEALTH_INFO));
			}
		}

		// Case Management
		private void NavigateThroughCaseManagementSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Case Management section"))
			{
				Follow(CASE_MANAGEMENT, Until(OFFICER_ASSIGNMENT_HISTORY));
			}
		}

		// Substance Abuse
		private void NavigateThroughSubstanceAbuseSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Substance Abuse section"))
			{
				Follow(SUBSTANCE_ABUSE, Until(SUBSTANCE_ABUSE_HISTORY));
			}
		}

		// Assessment History
		private void NavigateThroughAssessmentHistorySection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Assessment History section"))
			{
				Follow(ASSESSMENT_HISTORY, Until(MAYSI_HISTORY));
			}
		}

		// Chronological Notes
		private void NavigateThroughChronologicalNotesSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Chronological Notes section"))
			{
				Follow(CHRONOLOGICAL_NOTES, Until(CHRONOLOGICAL_NOTES_HISTORY));
				var notes = Locate(CHRONOLOGICAL_NOTES_HISTORY).LocateAll(CHRONOLOGICAL_NOTE);
				if (notes.Length > 0)
				{
					notes[0].Follow(DATE, Until(CHRONOLOGICAL_NOTE));
				}
			}
		}

		// CSR
		private void NavigateThroughCSRSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through CSR section"))
			{
				Follow(CSR, Until(CSR_HISTORY));
				var csrRecords = Locate(CSR_HISTORY).LocateAll(CSR);
				if (csrRecords.Length > 0)
				{
					csrRecords[0].Follow(CSR, Until(CSR));
				}
			}
		}

		// Supervision
		private void NavigateThroughSupervisionSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Supervision section"))
			{
				Follow(SUPERVISION, Until(SUPERVISION_HISTORY));
				var supervisions = Locate(SUPERVISION_HISTORY).LocateAll(SUPERVISION);
				if (supervisions.Length > 0)
				{
					supervisions[0].Follow(SUPERVISION, Until(SUPERVISION_DETAIL));
				}
			}
		}

		// Victim Services
		private void NavigateThroughVictimServicesSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Victim Services section"))
			{
				Follow(VICTIM_SERVICES, Until(VICTIM_SERVICES_HISTORY));
				var victimServices = Locate(VICTIM_SERVICES_HISTORY).LocateAll(VICTIM_SERVICE);
				if (victimServices.Length > 0)
				{
					victimServices[0].Follow(VICTIM_SERVICE, Until(VICTIM_SERVICE));
					GoBack();
				}
				Follow(MEDIATION, Until(MEDIATION_HISTORY));
				var mediationRecords = Locate(MEDIATION_HISTORY).LocateAll(MEDIATION_RECORD);
				if (mediationRecords.Length > 0)
				{
					mediationRecords[0].Follow(MEDIATION_DETAILS, Until(MEDIATION_DETAILS));
				}
			}
		}

		// Placement
		private void NavigateThroughPlacementSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Placement section"))
			{
				Follow(PLACEMENT, Until(PLACEMENT_HISTORY));
				var placements = Locate(PLACEMENT_HISTORY).LocateAll(PLACEMENT);
				if (placements.Length > 0)
				{
					placements[0].Follow(PLACEMENT, Until(PLACEMENT_DETAIL));
				}
			}
		}

		// Programs/Services
		private void NavigateThroughProgramsServicesSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Programs/Services section"))
			{
				Follow(PROGRAMS_SERVICES, Until(PROGRAMS_SERVICES_HISTORY));
				var programsServices = Locate(PROGRAMS_SERVICES_HISTORY).LocateAll(PROGRAM_SERVICE);
				if (programsServices.Length > 0)
				{
					programsServices[0].Follow(PROGRAM_SERVICE, Until(PROGRAM_SERVICE_DETAIL));
				}
			}
			;
		}

		//Detention
		private void NavigateThroughDetentionSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Detention section"))
			{
				Follow(DETENTION, Until(DETENTION_HISTORY));
				var detentions = Locate(DETENTION_HISTORY).LocateAll(DETENTION);
				if (detentions.Length > 0)
				{
					detentions[0].Follow(DETENTION, Until(DETENTION_DETAIL));
				}
			}
		}

		// Referrals
		private void NavigateThroughReferralsSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Referrals section"))
			{
				Follow(REFERRALS, Until(REFERRALS_HISTORY));
				var referrals = Locate(REFERRALS_HISTORY).LocateAll(REFERRAL);
				if (referrals.Length > 0)
				{
					referrals[0].Follow(REFERRAL, Until(REFERRAL_DETAIL));
				}
			}
		}

		// Schools
		private void NavigateThroughSchoolsSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Schools section"))
			{
				Follow(SCHOOLS, Until(SCHOOLS_HISTORY));
			}
		}

		// Juvenile's ID page sub nav bar
		private void NavigateThroughLinksInJuvenilePageSubNavBar()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Juvenile ID page subnav bar"))
			{
				Follow(ASSOCIATES, Until(ASSOCIATES));
				var associates = Locate(ASSOCIATES).LocateAll(ASSOCIATE);
				if (associates.Length > 0)
				{
					var associate = associates[0];
					associate.Follow(ASSOCIATE, Until(ASSOCIATE));
					ViewEmploymentRecord();
					GoBack();
				}
				Follow(ABUSE, Until(ABUSE_HISTORY));
				Follow(EMPLOYMENT, Until(EMPLOYMENT_HISTORY));
				ViewEmploymentRecord();
				Follow(MEDICAL, Until(MEDICAL_INFORMATION));
			}
		}

		// Offenses/Arrests
		private void NavigateThroughOffensesArrestsSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Offenses/Arrests section"))
			{
				Follow(OFFENSES_ARRESTS, Until(OFFENSES_HISTORY));
				Follow(OFFENSES, Until(OFFENSES_HISTORY));
				var offenses = Locate(OFFENSES_HISTORY).LocateAll(OFFENSE);
				if (offenses.Length > 0)
				{
					var offense = offenses[0];
					offense.Follow(OFFENSE, Until(OFFENSE_SUMMARY));
					Follow(OFFENSE_DETAILS, Until(OFFENSE_DETAILS));
					Follow(WARRANT, Until(WARRANT));
					Follow(COMPLAINANT, Until(COMPLAINANT));
					Follow(WITNESS, Until(WITNESS_LIST));
					Follow(EVIDENCE_PROPERTY, Until(EVIDENCE_PROPERTY_LIST));
					Follow(NARRATIVE, Until(NARRATIVE_HISTORY));
				}
				Follow(ARRESTS, Until(ARREST_HISTORY));
				var arrests = Locate(ARREST_HISTORY).LocateAll(ARREST);
				if (arrests.Length > 0)
				{
					var arrest = arrests[0];
					arrest.Follow(ARREST, Until(ARREST));
					Follow(OFFICERS_INVOLVED, Until(OFFICERS_INVOLVED_LIST));
					Follow(CUSTODY, Until(CUSTODY));
					Follow(ARREST_NARRATIVE, Until(ARREST_NARRATIVE_HISTORY));
				}
			}
		}

		private void NavigateThroughIntakeSection()
		{
			using (ExecutionSteps.Log.BeginSection("Navigate through Intake section"))
			{
				Follow(INTAKE, Until(INTAKE_HISTORY));
				var intakes = Locate(INTAKE_HISTORY).LocateAll(INTAKE);
				if (intakes.Length > 0)
				{
					intakes[0].Follow(INTAKE, Until(INTAKE_DETAIL));
				}
			}
		}

		private void ViewEmploymentRecord()
		{
			var employementRecords = Locate(EMPLOYMENT_HISTORY).LocateAll(EMPLOYER);
			if (employementRecords.Length > 0)
			{
				Follow(EMPLOYER);
				GoBack();
			}
		}
	}
}