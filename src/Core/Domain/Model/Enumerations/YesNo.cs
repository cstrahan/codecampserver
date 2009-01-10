namespace CodeCampServer.Core.Domain.Model.Enumerations
{
	public class YesNo : OrderedEnumeration
	{
		public static readonly YesNo Unknown = new YesNo(0, "Unknown", 2, null);
		public static readonly YesNo Yes = new YesNo(1, "Yes", 0, true);
		public static readonly YesNo No = new YesNo(2, "No", 1, false);

		public bool IsTrue
		{
			get { return BooleanValue != null && BooleanValue == true; }
		}

		public bool? BooleanValue { get; private set; }

		public YesNo()
		{
		}

		public YesNo(int value, string displayName, int displayOrder, bool? booleanValue)
			: base(value, displayName, displayOrder)
		{
			BooleanValue = booleanValue;
		}

		public static YesNo FromValue(bool? value)
		{
			return !value.HasValue ? Unknown : FromValue(value.Value);
		}

		public static YesNo FromValue(bool value)
		{
			return value ? Yes : No;
		}
	}
}