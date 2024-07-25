namespace Core;

public class SmsProviderEndpoint
{
	public SmsProviderEndpoint(string endpoint, string phoneNumber)
	{
		Endpoint = endpoint;
		PhoneNumber = phoneNumber;
	}

	public string Endpoint { get; }
	public string PhoneNumber { get; }
}
public interface ISmsProviderSettings
{
	public SmsProviderEndpoint Endpoint => new SmsProviderEndpoint(SmsProviderCommonSenderEndpoint,SmsProviderCommonSenderPhoneNumber);

	string SmsProviderBaseUrl{ get; } 
	string SmsProviderApiUsername { get; } 
    string SmsProviderApiKey { get; }
    string SmsProviderCommonSenderEndpoint { get; }
	string SmsProviderCommonSenderPhoneNumber { get; }
}
public interface IVerificationSmsSenderSettings
{
	public SmsProviderEndpoint Endpoint => new SmsProviderEndpoint(
		VerificationSmsSenderEndpoint ?? SmsProviderSettings.SmsProviderCommonSenderEndpoint,
		VerificationSmsSenderPhoneNumber ?? SmsProviderSettings.SmsProviderCommonSenderPhoneNumber
		);

	ISmsProviderSettings SmsProviderSettings { get; }
	int PhoneNumberConfirmTicketExpirationMinutes { get; set; }
	string VerificationSmsSenderEndpoint { get; }
	string VerificationSmsSenderPhoneNumber { get; }
    string VerificationSmsSenderTextTemplate { get; }
}

public interface ISubscriptionExpirationSmsNotifierSettings
{
	public SmsProviderEndpoint Endpoint => new SmsProviderEndpoint(
		SubscriptionExpirationSmsNotifierEndpoint ?? SmsProviderSettings.SmsProviderCommonSenderEndpoint,
		SubscriptionExpirationSmsNotifierPhoneNumber ?? SmsProviderSettings.SmsProviderCommonSenderPhoneNumber
	);

	ISmsProviderSettings SmsProviderSettings { get; }
	string SubscriptionExpirationSmsNotifierEndpoint { get; }
	string SubscriptionExpirationSmsNotifierPhoneNumber { get; }
    string SubscriptionExpirationSmsNotifierTextTemplate { get; }
}
