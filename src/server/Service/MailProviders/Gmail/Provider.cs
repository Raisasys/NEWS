
/*
using Core;
using MailKit.Net.Smtp;

namespace HostBase;

	
	using System.Net.Http;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using System;
using Microsoft.Extensions.Logging;
using MailKit.Security;
using System.Runtime;

	public class GoogleSendMailProvider : IEmailProvider
	{
		private readonly ILogger<GoogleSendMailProvider> _logger;
		private readonly IEmailSmtpSettings _settings;

		public GoogleSendMailProvider(ILogger<GoogleSendMailProvider> logger, IEmailSmtpSettings settings)
		{
			_logger = logger;
			_settings = settings;
		}


		public async Task<bool> Send(SendEmailCommand command)
		{
			var msg = new GmailSendMailMessageItem(command);
			return await SendMail(msg);
		}


		public async Task<bool> Send(SendMultipleEmailWithSameMessageCommand command)
		{
			var msg = new GmailSendMailMessageItem(command);
			return await SendMail(msg);
		}

		public async Task<SendEmailResponse[]> Send(SendMultipleEmailCommand command)
		{
			var result = new List<SendEmailResponse>();
			foreach (var item in command.Items)
			{
				var msg = new GmailSendMailMessageItem(item, command.FromEndpoint);
				var response = await SendMail(msg);
				result.Add(new SendEmailResponse(item.EmailBoxId,response));
			}
			return result.ToArray();
		}

		private async Task<bool> SendMail(GmailSendMailMessageItem command)
		{
			try
			{
			using var smtp = new SmtpClient();

			if (_settings.UseSSL)
			{
				await smtp.ConnectAsync(_settings.SmtpAddress, _settings.SmtpPort, SecureSocketOptions.SslOnConnect);
			}
			else if (_settings.UseStartTls)
			{
				await smtp.ConnectAsync(_settings.SmtpAddress, _settings.SmtpPort, SecureSocketOptions.StartTls);
			}
			await smtp.AuthenticateAsync(command.FromEndpoint.SenderEmailAddress, command.FromEndpoint.SenderEmailAppPassword);
			await smtp.SendAsync(command.GetMessage());
			await smtp.DisconnectAsync(true);
			return true;
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
				return false;
			}

		}

}
*/

