namespace CodeCampServer.Core.Domain
{
	public interface IAuditable
	{
		ChangeAuditInfo ChangeAuditInfo { get; set; }
	}
}