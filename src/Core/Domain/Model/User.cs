using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain.Model
{
	public class User : PersistentObject, IHasNaturalKey
	{
		public virtual string Username { get; set; }
		public virtual string Name { get; set; }
		public virtual string EmailAddress { get; set; }
		public virtual string PasswordHash { get; set; }
		public virtual string PasswordSalt { get; set; }

		public virtual string GetNaturalKey()
		{
			return "Username";
		}
	}
}