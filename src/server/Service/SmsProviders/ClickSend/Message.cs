/*
using System.Collections.Generic;
using Newtonsoft.Json;
using Core;

namespace HostBase;


public class ClickSendSmsConfig
{
		public ClickSendSmsConfig(string username, string password)
		{
			Username = username;
			Password = password;
		}

		public string Username { get; }
		public string Password { get; }

		public string AuthorizationHeaderKey => "Authorization";
		public string AuthorizationHeaderValue => "Basic " + (Username + ":" + Password).ToBase64();
	}

public class ClickSendSmsMessageItem
{
	public ClickSendSmsMessageItem() {}
	public ClickSendSmsMessageItem(string from, string body, string to)
	{
		From = from;
		Body = body;
		To = to;
	}

	[JsonProperty("from")]
	public string From { get; set; }

	[JsonProperty("body")]
	public string Body { get; set; }

	[JsonProperty("to")]
	public string To { get; set; }

	[JsonProperty("source")]
	public string Source => "sdk";

	
	public virtual string ToJson()
	{
		return JsonConvert.SerializeObject(this, Formatting.Indented);
	}

	
	public override int GetHashCode()
	{
		unchecked // Overflow is fine, just wrap
		{
			int hashCode = 41;
			if (this.From != null)
				hashCode = hashCode * 59 + this.From.GetHashCode();
			if (this.Body != null)
				hashCode = hashCode * 59 + this.Body.GetHashCode();
			if (this.To != null)
				hashCode = hashCode * 59 + this.To.GetHashCode();
			if (this.Source != null)
				hashCode = hashCode * 59 + this.Source.GetHashCode();
			return hashCode;
		}
	}

}

public class ClickSendSmsMessageCommand
{
	public ClickSendSmsMessageCommand() : this(new List<ClickSendSmsMessageItem>()){}

	public ClickSendSmsMessageCommand(List<ClickSendSmsMessageItem> messages)
	{
		Messages = messages;
	}

	public ClickSendSmsMessageCommand(SendSmsCommand command)
	{
		Messages = new List<ClickSendSmsMessageItem>{new ClickSendSmsMessageItem(command.From,command.Message,command.To)};
	}

	[JsonProperty("messages")]
	public List<ClickSendSmsMessageItem> Messages { get; set; }

	public virtual string ToJson()
	{
		return JsonConvert.SerializeObject(this, Formatting.Indented);
	}

	public override int GetHashCode()
	{
		unchecked // Overflow is fine, just wrap
		{
			int hashCode = 41;
			if (this.Messages != null)
				hashCode = hashCode * 59 + this.Messages.GetHashCode();
			return hashCode;
		}
	}

}
*/

