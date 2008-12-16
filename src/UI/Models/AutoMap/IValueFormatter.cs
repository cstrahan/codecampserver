namespace CodeCampServer.UI.Models.AutoMap
{
	public interface IValueFormatter
	{
		string FormatValue(object value, ResolutionContext context);
	}

}