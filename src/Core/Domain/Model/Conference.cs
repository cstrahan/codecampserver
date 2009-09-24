using System;
using System.Collections.Generic;
using System.Linq;
using CodeCampServer.Core.Domain.Model.Enumerations;

namespace CodeCampServer.Core.Domain.Model
{
	public class Conference : Event
	{
		public virtual string PhoneNumber { get; set; }
		public virtual string HtmlContent { get; set; }
		public virtual bool HasRegistration { get; set; }


	    public override string Title()
	    {
	        return Name;
	    }
	}
}