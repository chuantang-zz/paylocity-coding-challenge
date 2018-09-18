namespace Paylocity.Api.Repository.Test.Integration
{
	using System;
	using Bogus;
	using Model;

	public class EmployeeBuilder
	{
		public Employee BuildDefault()
		{
			var model = new Faker<Employee>()
				.RuleFor(fake => fake.FirstName, rule => rule.Person.FirstName)
				.RuleFor(fake => fake.LastName, rule => rule.Person.LastName)
				.RuleFor(fake => fake.ModifiedDate, rule => DateTime.UtcNow);

			return model;
		}
	}
}
