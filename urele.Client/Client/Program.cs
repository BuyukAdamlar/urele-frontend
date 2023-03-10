using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace urele.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");
			builder.Services.AddBlazoredLocalStorage();
			builder.Services.AddScoped<ClipboardService>();
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(API.url) });

			await builder.Build().RunAsync();
		}
	}
}