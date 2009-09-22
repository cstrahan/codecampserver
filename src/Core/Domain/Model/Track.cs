
namespace CodeCampServer.Core.Domain.Model
{
	public class Track : PersistentObject
	{
		public virtual Conference Conference { get; set; }
		public virtual string Name { get; set; }
	}
}