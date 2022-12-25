using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace urele.Client.Helper
{
	public class RunHttp
	{
		private static HttpClient Http = new HttpClient { BaseAddress = new Uri(API.url) };

		#region Get Methods
		public static async Task<bool> GetReturnless(string url)
		{
			var response = await Http.GetAsync(url);
			return response.IsSuccessStatusCode;
		}
		public static async Task<HttpResult<T>> Get<T>(string url)
		{
			var response = await Http.GetAsync(url);
			var result = await response.Content.ReadFromJsonAsync<T>();
			return getResult(result, response);
		}
		public static async Task<HttpResult<string>> GetString(string url)
		{
			var response = await Http.GetAsync(url);
			var result = await response.Content.ReadAsStringAsync();
			return getResult(result, response);
		}
		#endregion



		#region Post Methods
		public static async Task<bool> PostReturnless<T>(string url, params object[] content)
		{
			HttpContent hc;
			if (content.Length == 1)
			{
				hc = new StringContent("");
			}
			else if (content.Length == 1)
			{
				hc = new StringContent(JsonSerializer.Serialize(content[0]));
			}
			else
			{
				hc = new StringContent(JsonSerializer.Serialize(content));
			}
			var response = await Http.PostAsync(url, hc);
			return response.IsSuccessStatusCode;
		}
		public static async Task<HttpResult<T>> Post<T>(string url, params object[] content)
		{
			HttpResponseMessage rm;
			if (content.Length == 1)
			{
				rm = await Http.PostAsJsonAsync(url, content[0]);
			}
			else
			{
				rm = await Http.PostAsJsonAsync(url, content);
			}
			try
			{
				var result = await rm.Content.ReadFromJsonAsync<T>();
				return getResult(result, rm);
			}
			catch (Exception ex)
			{
				return getResult<T>(rm);
			}
		}
		public static async Task<HttpResult<string>> PostString(string url, params object[] content)
		{
			HttpContent hc;
			if (content.Length == 1)
			{
				hc = new StringContent("");
			}
			else if (content.Length == 1)
			{
				hc = new StringContent(JsonSerializer.Serialize(content[0]));
			}
			else
			{
				hc = new StringContent(JsonSerializer.Serialize(content));
			}
			var response = await Http.PostAsync(url, hc);
			var result = await response.Content.ReadAsStringAsync();
			return getResult(result, response);
		}
		#endregion



		#region Put Methods
		public static async Task<bool> PutReturnless<T>(string url, params object[] content)
		{
			HttpContent hc;
			if (content.Length == 1)
			{
				hc = new StringContent("");
			}
			else if (content.Length == 1)
			{
				hc = new StringContent(JsonSerializer.Serialize(content[0]));
			}
			else
			{
				hc = new StringContent(JsonSerializer.Serialize(content));
			}
			var response = await Http.PutAsync(url, hc);
			return response.IsSuccessStatusCode;
		}
		public static async Task<HttpResult<T>> Put<T>(string url, params object[] content)
		{
			HttpContent hc;
			if (content.Length == 1)
			{
				hc = new StringContent("");
			}
			else if (content.Length == 1)
			{
				hc = new StringContent(JsonSerializer.Serialize(content[0]));
			}
			else
			{
				hc = new StringContent(JsonSerializer.Serialize(content));
			}
			var response = await Http.PutAsync(url, hc);
			var result = await response.Content.ReadFromJsonAsync<T>();
			return getResult(result, response);
		}
		public static async Task<HttpResult<string>> PutString(string url, params object[] content)
		{
			HttpContent hc;
			if (content.Length == 1)
			{
				hc = new StringContent("");
			}
			else if (content.Length == 1)
			{
				hc = new StringContent(JsonSerializer.Serialize(content[0]));
			}
			else
			{
				hc = new StringContent(JsonSerializer.Serialize(content));
			}
			var response = await Http.PutAsync(url, hc);
			var result = await response.Content.ReadAsStringAsync();
			return getResult(result, response);
		}
		#endregion



		#region Delete Methods
		public static async Task<bool> DeleteReturnless(string url)
		{
			var response = await Http.DeleteAsync(url);
			return response.IsSuccessStatusCode;
		}
		public static async Task<HttpResult<T>> Delete<T>(string url)
		{
			var response = await Http.DeleteAsync(url);
			var result = await response.Content.ReadFromJsonAsync<T>();
			return getResult(result, response);
		}
		public static async Task<HttpResult<string>> DeleteString(string url)
		{
			var response = await Http.DeleteAsync(url);
			var result = await response.Content.ReadAsStringAsync();
			return getResult(result, response);
		}
		#endregion


		private static HttpResult<T> getResult<T>(T? result, HttpStatusCode statusCode)
		{
			HttpResult<T> res = new HttpResult<T>()
			{
				Result = result,
				status = statusCode,
			};
			int status = Convert.ToInt32(((int)statusCode).ToString()[0]);
			if (status == 2)
			{
				res.isSuccess = true;
			}
			else
			{
				res.isSuccess = false;
			}
			return res;
		}
		private static HttpResult<T> getResult<T>(T? result, HttpResponseMessage? resmas)
		{
			return new HttpResult<T>()
			{
				Result = result,
				status = resmas.StatusCode,
				isSuccess = resmas.IsSuccessStatusCode,
			};
		}
		private static HttpResult<T> getResult<T>(HttpResponseMessage? resmas)
		{
			return new HttpResult<T>()
			{
				status = resmas.StatusCode,
				isSuccess = resmas.IsSuccessStatusCode
			};
		}
	}
	public class HttpResult<T>
	{
		public T? Result { get; set; }
		public HttpStatusCode? status { get; set; }
		public bool isSuccess { get; set; }
	}
}
