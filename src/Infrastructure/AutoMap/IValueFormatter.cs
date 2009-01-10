namespace CodeCampServer.Infrastructure.AutoMap
{
	public interface IValueFormatter
	{
		string FormatValue(ResolutionContext context);
	}
}