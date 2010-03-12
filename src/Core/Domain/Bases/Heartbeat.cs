using System;
using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Domain.Bases
{
	public class Heartbeat : PersistentObjectOfGuid
	{
		public virtual string Message { get; set; }
		public virtual DateTime Date { get; set; }
	}
}