using Core;
namespace Service;

public class AppHttpClient
{
		private readonly string _url;
		private readonly IDictionary<string, object> _params;
		private readonly IDictionary<string, string> _headers;
		private TimeSpan _timeOut;
		private HttpClient _client;

		private AppHttpClient(string url)
		{
			_url = url;
			_params = new Dictionary<string, object>();
			_headers = new Dictionary<string, string>();
			_timeOut = 30.Minutes();
			_headers.Add("Content-Type", "application/json");
			_headers.Add("Accept", "application/json");
	}

		private AppHttpClient(HttpClient client, string url):this(url)
		{
			_client = client;
		}

	public static AppHttpClient Build(params string[] urls)
		{
			var url = string.Join(@"/", urls.Select(i => (i.EndsWith(@"/") ? i.Remove(i.Length - 1, 1) : i)));
			url = url.EndsWith(@"/") ? url.Remove(url.Length - 1, 1) : url;
			return new AppHttpClient(url);
		}

	public static AppHttpClient Build(HttpClient client, params string[] urls)
	{
		var url = string.Join(@"/", urls.Select(i => (i.EndsWith(@"/") ? i.Remove(i.Length - 1, 1) : i)));
		url = url.EndsWith(@"/") ? url.Remove(url.Length - 1, 1) : url;
		return new AppHttpClient(client,url);
	}
	public AppHttpClient AddParam(string key, object value)
		{
			if (_params.ContainsKey(key)) throw new CoreException("The key should not be duplicate");
			_params.Add(key, value);
			return this;
		}

		public AppHttpClient AddHeader(string key, string value)
		{
			if (_headers.ContainsKey(key)) throw new CoreException("The key should not be duplicate");
			_headers.Add(key, value);
			return this;
		}

		public AppHttpClient SetTimeOut(TimeSpan timeOut)
		{
			this._timeOut = timeOut;
			return this;
		}

		public async Task<HttpResponseMessage> Get()
		{
			_client ??= new HttpClient();
			PrepareHeader(_client);
			_client.Timeout = _timeOut;
			var result = await _client.GetAsync(PrepareUrl());
			return result;
		}

		public async Task<T> Get<T>()
		{
			var result = await Get();
			var jsonData = await result.Content.ReadAsStringAsync();
			var response = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonData);
			return response;
		}

		public async Task<HttpResponseMessage> Post(object data)
		{
		_client ??= new HttpClient();
		PrepareHeader(_client);
		_client.Timeout = _timeOut;
			var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
			var result = await _client.PostAsync(
				PrepareUrl(),
				new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json"));
			return result;
		}

		public async Task<T> Post<T>(object data)
		{
			var result = await Post(data);
			var jsonData = await result.Content.ReadAsStringAsync();
			var response = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonData);
			return response;
		}


		private string PrepareUrl()
		{
			if (_params.Any())
			{
				var queryString = string.Join("&", _params.Select(i => i.Key + "=" + i.Value));
				return _url + "?" + queryString;
			}
			return _url;
		}
		private void PrepareHeader(HttpClient client)
		{
			foreach (var header in _headers)
			{
				if (client.DefaultRequestHeaders.Contains(header.Key))
					client.DefaultRequestHeaders.Remove(header.Key);

				client.DefaultRequestHeaders.Add(header.Key, header.Value);
			}
		}

}