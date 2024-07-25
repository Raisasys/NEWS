
/*
using Core;

namespace HostBase;

	
	using System.Net.Http;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using System;
using Microsoft.Extensions.Logging;
using static Core.SendMultipleSmsWithSameMessageCommand;
using Humanizer;

	public class ClickSendSmsProvider : ISmsProvider
	{
		public static string HttpClientName = "SendSmsProvider";

	private readonly ILogger<ClickSendSmsProvider> _logger;
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ISmsProviderSettings _smsProviderSettings;

		private readonly ClickSendSmsConfig _config;

		public ClickSendSmsProvider(
			IHttpClientFactory httpClientFactory,
			ISmsProviderSettings smsProviderSettings, 
			ILogger<ClickSendSmsProvider> logger)
		{
			_logger = logger;
			_httpClientFactory = httpClientFactory;
			_smsProviderSettings = smsProviderSettings;
		_config = new ClickSendSmsConfig(_smsProviderSettings.SmsProviderApiUsername, _smsProviderSettings.SmsProviderApiKey);
		}


		public async Task<SendSmsResponse> Send(SendSmsCommand command)
		{
		var msg = new ClickSendSmsMessageCommand(command);
		var response = await SendSms(msg, command.Endpoint);
		return CreateResponseItem(response, command.SmsBoxId);

	}


		public async Task<SendSmsResponse[]> Send(SendMultipleSmsWithSameMessageCommand command)
		{
		var smsItems = new List<ClickSendSmsMessageItem>();
		foreach (var item in command.To)
		{
			smsItems.Add(new ClickSendSmsMessageItem(command.From, command.Message, item.To));
		}
		var msg = new ClickSendSmsMessageCommand(smsItems);
		var response = await SendSms(msg, command.Endpoint);
		return CreateResponseItems(response, command.To);

	}

		public async Task<SendSmsResponse[]> Send(SendMultipleSmsCommand command)
		{
			var smsItems = new List<ClickSendSmsMessageItem>();
			foreach (var item in command.Items)
			{
				smsItems.Add(new ClickSendSmsMessageItem(command.From, item.Message, item.To));
			}
			var msg = new ClickSendSmsMessageCommand(smsItems);
			var response = await SendSms(msg, command.Endpoint);
			return CreateResponseItems(response, command.Items);

		}

	private async Task<ClickSendSmsResponse> SendSms(ClickSendSmsMessageCommand command, SmsProviderEndpoint endpoint)
		{
			try
			{
				var client = _httpClientFactory.CreateClient(HttpClientName);
				var response = await AppHttpClient
					.Build(client, _smsProviderSettings.SmsProviderBaseUrl, endpoint.Endpoint)
					.AddHeader(_config.AuthorizationHeaderKey, _config.AuthorizationHeaderValue)
					.Post<ClickSendSmsResponse>(command);

				return response;
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
			}

			return new ClickSendSmsResponse 
			{
				HttpCode= 500
			};

		}

		public SendSmsResponse[] CreateResponseItems(ClickSendSmsResponse response,SmsItem[] toSmsBoxIds)
		{
			var resultItems = new List<SendSmsResponse>();

		if (response.Successed)
		{
			foreach (var to in toSmsBoxIds)
			{
				var responseToItem = response.Data.Messages.FirstOrDefault(i => i.To== to.To);
				if (responseToItem != null && responseToItem.Successed)
					resultItems.Add(SendSmsResponse.Success(to.SmsBoxId));
				else
					resultItems.Add(SendSmsResponse.Failed(to.SmsBoxId));
			}
		}

		return resultItems.ToArray();

		}

		public SendSmsResponse CreateResponseItem(ClickSendSmsResponse response, Guid smsBoxId)
		{
		if (response.Successed)
		{
			var responseToItem = response.Data.Messages.FirstOrDefault();
			if (responseToItem != null && responseToItem.Successed)
				return SendSmsResponse.Success(smsBoxId);
		}
		return SendSmsResponse.Failed(smsBoxId);
	}
}
*/

