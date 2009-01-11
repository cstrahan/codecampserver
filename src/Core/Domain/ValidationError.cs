namespace CodeCampServer.Core.Domain
{
	public class ValidationError : ValueObject<ValidationError>
	{
		public string Key { get; set; }
		public string Message { get; set; }
	}
}