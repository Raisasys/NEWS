/*
using System.Collections.Generic;
using System.Linq;
using Core;
using MimeKit;
using static Core.SendMultipleEmailCommand;

namespace HostBase;


public class GmailSendMailMessageItem
{
	public GmailSendMailMessageItem(MultipleEmailItem item, EmailProviderEndpoint fromEndpoint)
	{
		To = new []{ item.To };
		FromEndpoint = fromEndpoint;
		Subject = item.Subject;
		Body = item.Message;
	}

	public GmailSendMailMessageItem(SendMultipleEmailWithSameMessageCommand command)
	{
		To = command.To.Select(i => i.To).ToArray();
		FromEndpoint = command.FromEndpoint;
		Subject = command.Subject;
		Body = command.Message;
	}
	public GmailSendMailMessageItem(SendEmailCommand command)
	{
		To = new[] { command.To };
		FromEndpoint = command.FromEndpoint;
		Subject = command.Subject;
		Body = command.Message;
	}
	public GmailSendMailMessageItem(EmailProviderEndpoint fromEndpoint, string subject, string body, params EmailToEndpoint[] to)
	{
		To = to ;
		FromEndpoint = fromEndpoint;
		Subject = subject;
		Body = body;
	}

	public EmailToEndpoint[]  To { get; }
	public EmailProviderEndpoint FromEndpoint { get;}
	public string Subject { get; }
	public string Body { get; }

	public virtual MimeMessage GetMessage()
	{
		var message = new MimeMessage();
		message.From.Add(new MailboxAddress(FromEndpoint.SenderEmailName, FromEndpoint.SenderEmailAddress));
		foreach (var toEndpoint in To)
		{
			message.To.Add(new MailboxAddress(toEndpoint.EmailName, toEndpoint.EmailAddress));
		}
		
		message.Subject = Subject;


		var textPartBody = new TextPart("plain") { Text = Body };
		//var body = new BodyBuilder();
		//body.HtmlBody = Body;
		//message.Body = body.ToMessageBody();
		message.Body = textPartBody;

		return message;
	}
}
*/
