
using System.Net;
using static Core.SendMultipleEmailWithSameMessageCommand;

namespace Core;

public class EmailToEndpoint
{
	public EmailToEndpoint(string emailAddress, string emailName)
	{
		EmailAddress = emailAddress;
		EmailName = emailName;
	}

	public string EmailAddress { get; }
	public string EmailName { get; }
}

public class SendEmailResponse
{
	public static SendEmailResponse Success(Guid emailBoxId) => new SendEmailResponse(emailBoxId, true);
	public static SendEmailResponse Failed(Guid emailBoxId) => new SendEmailResponse(emailBoxId, false);
	public SendEmailResponse(Guid emailBoxId, bool successed)
	{
		EmailBoxId = emailBoxId;
		Successed = successed;
	}

	public Guid EmailBoxId { get; }
	public bool Successed { get; }
}
public abstract class SendEmailCommandBase
{
		protected SendEmailCommandBase(EmailProviderEndpoint fromEndpoint)
		{
			FromEndpoint = fromEndpoint;
		}

		
		public EmailProviderEndpoint FromEndpoint { get; }
	}

	public class SendEmailCommand : SendEmailCommandBase
{
	public Guid EmailBoxId { get; set; }
	public EmailToEndpoint To { get; }
		public string Message { get; }
		public string Subject { get; }

		public SendEmailCommand(EmailProviderEndpoint endpoint, EmailToEndpoint to, Guid emailBoxId, string subject, string message) : base(endpoint)
		{
			EmailBoxId = emailBoxId;
			To = to;
			Subject = subject;
			Message = message;
		}
	}

	public class SendMultipleEmailWithSameMessageCommand : SendEmailCommandBase
	{
		public EmailItem[] To { get; }
		public string Message { get; }
		public string Subject { get; set; }

	public SendMultipleEmailWithSameMessageCommand(EmailProviderEndpoint endpoint, EmailItem[] to, string message) : base(endpoint)
		{
			To = to;
			Message = message;
		}

		public class EmailItem
		{
			public EmailItem(Guid emailBoxId, EmailToEndpoint to)
			{
				EmailBoxId = emailBoxId;
				To = to;
			}
			public Guid EmailBoxId { get; set; }
			public EmailToEndpoint To { get; }
		}
}

	public class SendMultipleEmailCommand : SendEmailCommandBase
{
		public SendMultipleEmailCommand(EmailProviderEndpoint endpoint, MultipleEmailItem[] items) : base(endpoint)
	{
			Items = items;
		}

		public MultipleEmailItem[] Items{ get; }

	public class MultipleEmailItem: EmailItem
	{
		public MultipleEmailItem(Guid emailBoxId, EmailToEndpoint to, string subject, string message):base(emailBoxId,to)
		{
			Subject= subject;
			Message = message;
		}
		public string Subject { get;  }
		public string Message { get; }
	}
}