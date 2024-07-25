using Core.Events;

namespace Events;

public class SendVerificationLinkMailEvent : Event<EmailEventBusChannel>
{
	public SendVerificationLinkMailEvent()
	{
	}

	public SendVerificationLinkMailEvent(Guid mailBoxId)
	{
		MailBoxId = mailBoxId;
	}

	public Guid MailBoxId { get;  }
}