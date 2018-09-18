namespace Paylocity.Api.Repository.Test.Integration
{
	using System;
	using Bogus;
	using Model;

	public class DependentBuilder
	{
		public Dependent BuildDefault()
		{
			var model = new Faker<Dependent>()
				.RuleFor(fake => fake.FirstName, rule => rule.Person.FirstName)
				.RuleFor(fake => fake.LastName, rule => rule.Person.LastName)
				.RuleFor(fake => fake.ModifiedDate, rule => DateTime.UtcNow);

			return model;
		}
	}
}
