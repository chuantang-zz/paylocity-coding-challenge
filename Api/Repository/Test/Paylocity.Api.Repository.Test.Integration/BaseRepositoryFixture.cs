namespace Paylocity.Api.Repository.Test.Integration
{
	using System;
	using System.IO;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Repository;

	public class BaseRepositoryFixture : IDisposable
	{
		private bool _objectDisposed;

		internal PaylocityContext Context;

		public BaseRepositoryFixture()
		{
			var directory = Directory.GetCurrentDirectory().Replace("bin\\Debug\\netcoreapp2.1", string.Empty);

			// get the configuration from the app settings
			var config = new ConfigurationBuilder()
				.SetBasePath(directory)
				.AddJsonFile("appsettings.json")
				.Build();

			// define the database to use
			var optionsBuilder = new DbContextOptionsBuilder<PaylocityContext>();
			optionsBuilder.EnableSensitiveDataLogging();
			optionsBuilder.UseSqlServer(config.GetConnectionString("PaylocityConnection"));

			Context = new PaylocityContext(optionsBuilder.Options);
		}

		#region DESTRUCTOR
			~BaseRepositoryFixture() { Dispose(false); }
		#endregion

		#region IDISPOSABLE IMPLEMENTATION
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			protected virtual void Dispose(bool disposing)
			{
				if (_objectDisposed) return;

				if (disposing)
				{
					Context.Dispose();
					Context = null;
				}

				_objectDisposed = true;
			}
		#endregion
	}
}
