namespace CodeCampServer.Core.Domain.Model.Enumerations
{
	public class OrderedEnumeration : Enumeration
	{
		public OrderedEnumeration(int value, string displayName, int displayOrder) : base(value, displayName)
		{
			DisplayOrder = displayOrder;
		}

		public OrderedEnumeration(int value, string displayName) : this(value, displayName, value)
		{
		}

		public OrderedEnumeration()
		{
		}

		public int DisplayOrder { get; private set; }
	}
}