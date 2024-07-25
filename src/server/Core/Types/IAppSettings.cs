namespace Core;

public interface IAppSettings
{
	public static string AppSettingsId = Guid.Empty.ToString();

	public string TermsAndConditions { get; }
}



