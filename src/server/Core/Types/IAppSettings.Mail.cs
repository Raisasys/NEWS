namespace Core;

public class EmailProviderEndpoint
{
	public EmailProviderEndpoint(string senderEmailAddress, string senderEmailName, string senderEmailAppPassword)
	{
		SenderEmailAddress = senderEmailAddress;
		SenderEmailName = senderEmailName;
		SenderEmailAppPassword = senderEmailAppPassword;
	}


	public string SenderEmailAddress { get; }
	public string SenderEmailName { get; }
	public string SenderEmailAppPassword { get; }
}

public interface IEmailSmtpSettings
{
    string SmtpAddress { get; }
	int SmtpPort { get; }
    bool UseSSL { get; }
    bool UseStartTls { get; }
	
    string EmailProviderCommonSenderEmailAddress { get; }
    string EmailProviderCommonSenderEmailName { get; }
    string EmailProviderCommonSenderEmailAppPassword { get; }

	public EmailProviderEndpoint Endpoint => new EmailProviderEndpoint(EmailProviderCommonSenderEmailAddress,EmailProviderCommonSenderEmailName,EmailProviderCommonSenderEmailAppPassword);

}

public interface IVerificationEmailLinkSenderSettings
{
	public EmailProviderEndpoint Endpoint => new EmailProviderEndpoint(
		VerificationSenderEmailAddress ?? EmailSmtpSettings.EmailProviderCommonSenderEmailAddress,
		VerificationSenderEmailName ?? EmailSmtpSettings.EmailProviderCommonSenderEmailName,
		VerificationSenderEmailAppPassword ?? EmailSmtpSettings.EmailProviderCommonSenderEmailAppPassword);

	IEmailSmtpSettings EmailSmtpSettings { get; }

	int EmailConfirmTicketExpirationHours { get; set; }
	string VerificationSenderEmailAddress { get; }
    string VerificationSenderEmailName { get; }
    string VerificationSenderEmailAppPassword { get; }
    string VerificationEmailVerifyHeaderTemplate { get; }
    string VerificationEmailVerifyBodyTemplate { get; }
    string VerificationEmailConfirmedHeaderTemplate { get; }
    string VerificationEmailConfirmedBodyTemplate { get; }

}

public interface ISubscriptionExpirationEmailNotifierSettings
{
	public EmailProviderEndpoint Endpoint => new EmailProviderEndpoint(
		SubscriptionExpirationSenderEmailAddress ?? EmailSmtpSettings.EmailProviderCommonSenderEmailAddress,
		SubscriptionExpirationSenderEmailName ?? EmailSmtpSettings.EmailProviderCommonSenderEmailName,
		SubscriptionExpirationSenderEmailAppPassword ?? EmailSmtpSettings.EmailProviderCommonSenderEmailAppPassword);

	IEmailSmtpSettings EmailSmtpSettings { get; }
    string SubscriptionExpirationSenderEmailAddress { get; }
    string SubscriptionExpirationSenderEmailName { get; }
    string SubscriptionExpirationSenderEmailAppPassword { get; }
    string SubscriptionExpirationEmailHeaderTemplate { get; }
    string SubscriptionExpirationEmailBodyTemplate { get; }
}

public interface INewsEmailSenderSettings
{
	public EmailProviderEndpoint Endpoint => new EmailProviderEndpoint(
		NewsSenderEmailAddress ?? EmailSmtpSettings.EmailProviderCommonSenderEmailAddress,
		NewsSenderEmailName ?? EmailSmtpSettings.EmailProviderCommonSenderEmailName,
		NewsSenderEmailAppPassword ?? EmailSmtpSettings.EmailProviderCommonSenderEmailAppPassword);


	IEmailSmtpSettings EmailSmtpSettings { get; }
    string NewsSenderEmailAddress { get; }
    string NewsSenderEmailName { get; }
    string NewsSenderEmailAppPassword { get; }
    string NewsEmailHeaderTemplate { get; }
    string NewsEmailBodyTemplate { get; }

}
