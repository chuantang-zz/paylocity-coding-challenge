namespace Paylocity.Api.Website.Test.Integration
{
	using System.IO;
	using System.Net.Http;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.TestHost;
	using Microsoft.Extensions.Configuration;

	public class BaseControllerFixture
	{
		internal readonly HttpClient Client;

		protected BaseControllerFixture()
		{
			// Arrange
			var webHostBuilder = new WebHostBuilder()
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					var directory = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp2.1", string.Empty);
					config.AddJsonFile("appsettings.json").SetBasePath(directory);
				})
				.UseStartup<Startup>();

			var server = new TestServer(webHostBuilder);
			Client = server.CreateClient();
		}
	}
}
