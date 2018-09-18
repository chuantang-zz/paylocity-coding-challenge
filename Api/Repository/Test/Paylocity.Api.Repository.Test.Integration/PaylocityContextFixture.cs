namespace Paylocity.Api.Repository.Test.Integration
{
	using FluentAssertions;
	using Microsoft.EntityFrameworkCore;
	using Repository;
	using Xunit;

	[Trait("Repository", "Paylocity DB Context")]
	public class PaylocityContextFixture : BaseRepositoryFixture
	{
		[Fact]
		public void AssetMContext_Creation_ValidateConnectionStringIsSet()
		{
			Context.Should().BeOfType<PaylocityContext>();
			Context.Database.GetDbConnection().ConnectionString.Should().NotBeNullOrWhiteSpace();
		}
	}
}
