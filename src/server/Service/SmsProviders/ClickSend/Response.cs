/*
using Newtonsoft.Json;

namespace HostBase;



public class ClickSendSmsResponse
{
	[JsonProperty("http_code")]
	public int HttpCode { get; set; }

	[JsonProperty("response_code")]
	public string ResponseCode { get; set; }

	[JsonProperty("response_msg")]
	public string ResponseMsg { get; set; }

	[JsonProperty("data")]
	public ClickSendSmsResponseData Data { get; set; }


	[JsonIgnore]
	public bool Successed => HttpCode == 200;


}
public class ClickSendSmsResponseData
{

	[JsonProperty("messages")]
	public ClickSendSmsResponseMessage[] Messages { get; set; }

}
public class ClickSendSmsResponseMessage
{

	[JsonProperty("to")]
	public string To { get; set; }


	[JsonProperty("status")]
	public string Status { get; set; }

	[JsonIgnore]
	public bool Successed => Status?.ToUpper() == "SUCCESS";
}
*/

