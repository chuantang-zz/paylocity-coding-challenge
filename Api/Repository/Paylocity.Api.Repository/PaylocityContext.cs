namespace Paylocity.Api.Repository
{
	using Microsoft.EntityFrameworkCore;
	using Model;

	public class PaylocityContext : DbContext
	{
		public PaylocityContext(DbContextOptions<PaylocityContext> options) : base(options) { }

		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<Dependent> Dependents { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//// get the configuration from the app settings
			//var config = new ConfigurationBuilder()
			//	.SetBasePath(Directory.GetCurrentDirectory())
			//	.AddJsonFile("appsettings.json")
			//	.Build();

			//// define the database to use
			//optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.Entity<State>();
			//.HasOne(state => state.Country)
			//.WithOne(country => country.State)
			//.HasConstraintName("ForeignKey_State_Country");
		}
	}
}
