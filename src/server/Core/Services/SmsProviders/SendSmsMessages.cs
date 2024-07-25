
using System.Net;
using static Core.SendMultipleSmsWithSameMessageCommand;

namespace Core;

	
	public class SendSmsResponse
	{
		public static SendSmsResponse Success(Guid smsBoxId)=> new SendSmsResponse(smsBoxId, true);
		public static SendSmsResponse Failed(Guid smsBoxId)=> new SendSmsResponse(smsBoxId, false);
		public SendSmsResponse(Guid smsBoxId, bool successed)
		{
			SmsBoxId = smsBoxId;
			Successed = successed;
		}

		public Guid SmsBoxId{ get; }
		public bool Successed { get; }
}
public abstract class SendSmsCommandBase
	{
		protected SendSmsCommandBase(SmsProviderEndpoint endpoint)
		{
			From = endpoint.PhoneNumber;
			Endpoint = endpoint;
		}
	
		public SmsProviderEndpoint Endpoint { get; }
		public string From { get; }
	}

	public class SendSmsCommand : SendSmsCommandBase
	{
		public Guid SmsBoxId { get; set; }
	public string To { get; }
		public string Message { get; }

		public SendSmsCommand(SmsProviderEndpoint endpoint,Guid smsBoxId , string to, string message) : base(endpoint)
		{
			To = to;
			SmsBoxId = smsBoxId;
			Message = message;
		}
	}

	public class SendMultipleSmsWithSameMessageCommand : SendSmsCommandBase
	{
		public SmsItem[] To { get; }
		public string Message { get; }

		public SendMultipleSmsWithSameMessageCommand(SmsProviderEndpoint endpoint, SmsItem[] to, string message) : base(endpoint)
		{
			To = to;
			Message = message;
		}

		public class SmsItem
		{
			public SmsItem(Guid smsBoxId, string to)
			{
				SmsBoxId = smsBoxId;
				To = to;
			}
			public Guid SmsBoxId { get; set; }
			public string To { get; }
		}
}

	public class SendMultipleSmsCommand : SendSmsCommandBase
{
		public SendMultipleSmsCommand(SmsProviderEndpoint endpoint, MultipleSmsItem[] items) : base(endpoint)
	{
			Items = items;
		}

		public MultipleSmsItem[] Items{ get; }

		public class MultipleSmsItem : SmsItem
	{
			public MultipleSmsItem(Guid smsBoxId, string to, string message):base(smsBoxId, to)
			{
				Message = message;
			}
		public string Message { get; }
		}
}