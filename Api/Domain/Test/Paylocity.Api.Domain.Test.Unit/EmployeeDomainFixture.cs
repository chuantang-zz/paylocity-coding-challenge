namespace Paylocity.Api.Domain.Test.Unit
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Bogus;
	using Domain;
	using FluentAssertions;
	using Interface;
	using Model;
	using Moq;
	using Repository.Interface;
	using Repository.Model;
	using Xunit;

	[Trait("Domain", "Employee Domain")]
	public class EmployeeDomainFixture : BaseDomain
	{
		private readonly IEmployeeDomain _domain;
		private readonly Mock<IEmployeeRepository> _repository;
		private readonly Mock<IDependentRepository> _repositoryDependent;

		public EmployeeDomainFixture()
		{
			_repository = new Mock<IEmployeeRepository>();
			_repositoryDependent = new Mock<IDependentRepository>();

			_domain = new EmployeeDomain(_repository.Object, _repositoryDependent.Object);
		}

		#region DELETE
			[Fact]
			public void DeleteAsync_WithNullEmployeeParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _domain.DeleteAsync(Guid.Empty);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void DeleteAsync_GivenEmployeeExist_ReturnDeletedEmployee()
			{
				var domainModel = CreateDomain();
				var model = ToModel<Employee, EmployeeDomainModel>(domainModel);

			_repository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(model);

				var result = await _domain.DeleteAsync(Guid.NewGuid()).ConfigureAwait(false);
				result.Should().BeOfType<EmployeeDomainModel>();
				result.Should().BeEquivalentTo(domainModel);
			}
		#endregion

		#region GET
			[Fact]
			public async void GetAsync_ReturnAll()
			{
				var domainModels = new List<EmployeeDomainModel> { CreateDomain(), CreateDomain(), CreateDomain() };
				var models = ToListModel<Employee, EmployeeDomainModel>(domainModels);

				_repository.Setup(x => x.GetAsync()).ReturnsAsync(models);

				var result = await _domain.GetAsync().ConfigureAwait(false);
				result.Should().AllBeOfType<EmployeeDomainModel>();
				result.Should().BeEquivalentTo(domainModels);
			}
		#endregion

		#region GET BY ID
			[Fact]
			public void GetAsync_ById_WithDefaultGuidEmployeeIdParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _domain.GetAsync(Guid.Empty);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void GetAsync_GivenEmployeeIdExist_ReturnEmployee()
			{
				var domainModel = CreateDomain();
				var model = ToModel<Employee, EmployeeDomainModel>(domainModel);

			_repository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(model);

				var result = await _domain.GetAsync(Guid.NewGuid()).ConfigureAwait(false);
				result.Should().BeOfType<EmployeeDomainModel>();
				result.Should().BeEquivalentTo(domainModel);
			}
		#endregion

		#region INSERT
			[Fact]
			public void InsertAsync_WithNullEmployeeParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _domain.InsertAsync(null);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void InsertAsync_GivenEmployeeDoesNotExist_ReturnInsertedEmployee()
			{
				var domainModel = CreateDomain();
				var model = ToModel<Employee, EmployeeDomainModel>(domainModel);

				_repository.Setup(x => x.InsertAsync(It.IsAny<Employee>())).ReturnsAsync(model);

				var result = await _domain.InsertAsync(new EmployeeDomainModel()).ConfigureAwait(false);
				result.Should().BeOfType<EmployeeDomainModel>();
				result.Should().BeEquivalentTo(domainModel);
			}
		#endregion

		#region UPDATE
			[Fact]
			public void UpdateAsync_WithNullEmployeeParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _domain.UpdateAsync(null);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void UpdateAsync_GivenEmployeeDoesNotExist_ReturnNullEmployee()
			{
				_repository.Setup(x => x.UpdateAsync(It.IsAny<Employee>())).ReturnsAsync((Employee)null);

				var result = await _domain.UpdateAsync(new EmployeeDomainModel()).ConfigureAwait(false);
				result.Should().BeNull();
			}

			[Fact]
			public async void UpdateAsync_GivenEmployeeExist_ReturnUpdatedEmployee()
			{
				var domainModel = CreateDomain();
				var model = ToModel<Employee, EmployeeDomainModel>(domainModel);
				model.FirstName = Guid.NewGuid().ToString();
				model.LastName = Guid.NewGuid().ToString();

				_repository.Setup(x => x.UpdateAsync(It.IsAny<Employee>())).ReturnsAsync(model);

				var result = await _domain.UpdateAsync(new EmployeeDomainModel()).ConfigureAwait(false);

				AssertionOptions.AssertEquivalencyUsing(options => options.ExcludingMissingMembers());
				result.Should().BeOfType<EmployeeDomainModel>();
				result.Should().NotBe(domainModel);
				result.Should().BeEquivalentTo(model);
			}
		#endregion

		private EmployeeDomainModel CreateDomain()
		{
			var domainModel = new Faker<EmployeeDomainModel>()
				.RuleFor(fake => fake.FirstName, rule => rule.Person.FirstName)
				.RuleFor(fake => fake.LastName, rule => rule.Person.LastName)
				.RuleFor(fake => fake.ModifiedDate, rule => DateTime.UtcNow);

			return domainModel.Generate();
		}
	}
}
