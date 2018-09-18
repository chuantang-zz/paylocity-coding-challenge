namespace Paylocity.Api.Website
{
	using System;
	using System.Reflection;
	using Autofac;
	using Autofac.Extensions.DependencyInjection;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Paylocity.Api.Repository;
	using Swashbuckle.AspNetCore.Swagger;

	public class Startup
	{
		private IConfiguration Configuration { get; }
		private IContainer ApplicationContainer { get; set; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			ConfigureCors(services);

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			ConfigureSwagger(services);
			ConfigureDatabases(services);

			// Create the container builder
			var builder = new ContainerBuilder();

			ConfigureAutoFac(builder, services);

			// Note that Populate is basically a foreach to add things into Autofac that are in the collection. If you register things in Autofac BEFORE Populate then the stuff in the ServiceCollection can override those things; 
			// if you register AFTER Populate those registrations can override things in the ServiceCollection. Mix and match as needed.
			builder.Populate(services);

			ApplicationContainer = builder.Build();

			// Create the IServiceProvider based on the container.
			return new AutofacServiceProvider(ApplicationContainer);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();
			else
				app.UseHsts();

			app.UseHttpsRedirection();

			// Shows UseCors with named policy.
			app.UseCors("AllowAllOrigin");

			app.UseAuthentication();
			app.UseSwagger();
			app.UseSwaggerUI
			(
				c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "Paylocity v1");
					c.DocumentTitle = "Paylocity API v1";
				}
			);

			app.UseMvc();

			// If you want to dispose of resources that have been resolved in the application container, register for the "ApplicationStopped" event.
			appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
		}

		private void ConfigureCors(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy
				(
					"AllowAllOrigin",
					corBuilder => corBuilder
						.AllowAnyOrigin()
						.AllowAnyHeader()
						.AllowAnyMethod()
						.AllowCredentials()
				);
			});
		}

		private void ConfigureDatabases(IServiceCollection services)
		{
			//services.AddDbContext<AssetMIdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AssetMConnection")));
			services.AddDbContext<PaylocityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PaylocityConnection")));

			//services.AddIdentity<ApplicationUser, ApplicationRole>()
			//		.AddEntityFrameworkStores<AssetMIdentityContext>()
			//		.AddDefaultTokenProviders()
			//		.AddUserStore<UserStore<ApplicationUser, ApplicationRole, AssetMIdentityContext, Guid>>()
			//		.AddRoleStore<RoleStore<ApplicationRole, AssetMIdentityContext, Guid>>();
		}

		private void ConfigureSwagger(IServiceCollection services)
		{
			services.AddSwaggerGen
			(
				options => 
				{
					options.SwaggerDoc("v1", new Info { Title = "Paylocity Api", Version = "v1" });
				}
			);
		}

		private void ConfigureAutoFac(ContainerBuilder builder, IServiceCollection services)
		{
			// Register domain dependencies.
			var domainAssemblyName = new AssemblyName("Paylocity.Api.Domain");
			Assembly domainAssembly = Assembly.Load(domainAssemblyName);
			builder.RegisterAssemblyTypes(domainAssembly).Where(t => t.Name.EndsWith("Domain")).AsImplementedInterfaces();

			// Register repository dependencies.
			var repositoryAssemblyName = new AssemblyName("Paylocity.Api.Repository");
			Assembly repositoryAssembly = Assembly.Load(repositoryAssemblyName);
			builder.RegisterAssemblyTypes(repositoryAssembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
			builder.RegisterAssemblyTypes(repositoryAssembly).Where(t => t.Name.EndsWith("Context")).As<DbContext>().AsSelf();
		}
	}
}
