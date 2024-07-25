
namespace Core;

public interface ISmsProvider
{
	Task<SendSmsResponse> Send(SendSmsCommand command);
	Task<SendSmsResponse[]> Send(SendMultipleSmsWithSameMessageCommand command);
	Task<SendSmsResponse[]> Send(SendMultipleSmsCommand command);
}