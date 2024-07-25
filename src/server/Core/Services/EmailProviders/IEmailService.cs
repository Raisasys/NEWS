namespace Core;

public interface IEmailProvider
{
	Task<bool> Send(SendEmailCommand command);
	Task<bool> Send(SendMultipleEmailWithSameMessageCommand command);
	Task<SendEmailResponse[]> Send(SendMultipleEmailCommand command);
}

