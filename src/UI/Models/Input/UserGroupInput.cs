using System;
using System.Collections.Generic;
using CodeCampServer.UI.Helpers.Validation.Attributes;

namespace CodeCampServer.UI.Models.Input
{
	public class UserGroupInput
	{
		[Required("User Group Key")]
		[ValidateKey]
		public virtual string Key { get; set; }

		public virtual Guid Id { get; set; }

		[Required("Name")]
		public virtual string Name { get; set; }

		public virtual string DomainName { get; set; }
		public virtual string HomepageHTML { get; set; }

		public virtual string City { get; set; }

		public virtual string Region { get; set; }

		public virtual string Country { get; set; }

		public virtual string GoogleAnalysticsCode { get; set; }

		public virtual IList<UserSelectorInput> Users { get; set; }

		public virtual IList<SponsorInput> Sponsors { get; set; }

		public string Location()
		{
			string location = "";
			if (!string.IsNullOrEmpty(City))
			{
				location = City + ", " + Region + " - ";
			}
			location += Country;
			return location;
		}
	}
}