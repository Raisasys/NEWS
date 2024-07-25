using Core.Events;

namespace Events;

public class SendVerificationSmsCodeEvent : Event<SmsEventBusChannel>
{
	public SendVerificationSmsCodeEvent(){}

	public SendVerificationSmsCodeEvent(Guid smsBoxId)
	{
		SmsBoxId = smsBoxId;
	}

	public Guid SmsBoxId { get; }
}