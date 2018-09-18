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

	[Trait("Domain", "Dependent Domain")]
	public class DependentDomainFixture : BaseDomain
	{
		private readonly IDependentDomain _domain;
		private readonly Mock<IDependentRepository> _repository;

		public DependentDomainFixture()
		{
			_repository = new Mock<IDependentRepository>();

			_domain = new DependentDomain(_repository.Object);
		}

		#region DELETE
			[Fact]
			public void DeleteAsync_WithNullDependentParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _domain.DeleteAsync(Guid.Empty);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void DeleteAsync_GivenDependentExist_ReturnDeletedDependent()
			{
				var domainModel = CreateDomain();
				var model = ToModel<Dependent, DependentDomainModel>(domainModel);

			_repository.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(model);

				var result = await _domain.DeleteAsync(Guid.NewGuid()).ConfigureAwait(false);
				result.Should().BeOfType<DependentDomainModel>();
				result.Should().BeEquivalentTo(domainModel);
			}
		#endregion

		#region GET
			[Fact]
			public async void GetAsync_ReturnAll()
			{
				var domainModels = new List<DependentDomainModel> { CreateDomain(), CreateDomain(), CreateDomain() };
				var models = ToListModel<Dependent, DependentDomainModel>(domainModels);

				_repository.Setup(x => x.GetAsync()).ReturnsAsync(models);

				var result = await _domain.GetAsync().ConfigureAwait(false);
				result.Should().AllBeOfType<DependentDomainModel>();
				result.Should().BeEquivalentTo(domainModels);
			}
		#endregion

		#region GET BY ID
			[Fact]
			public void GetAsync_ById_WithDefaultGuidDependentIdParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _domain.GetAsync(Guid.Empty);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void GetAsync_GivenDependentIdExist_ReturnDependent()
			{
				var domainModel = CreateDomain();
				var model = ToModel<Dependent, DependentDomainModel>(domainModel);

			_repository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(model);

				var result = await _domain.GetAsync(Guid.NewGuid()).ConfigureAwait(false);
				result.Should().BeOfType<DependentDomainModel>();
				result.Should().BeEquivalentTo(domainModel);
			}
		#endregion

		#region INSERT
			[Fact]
			public void InsertAsync_WithNullDependentParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _domain.InsertAsync(null);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void InsertAsync_GivenDependentDoesNotExist_ReturnInsertedDependent()
			{
				var domainModel = CreateDomain();
				var model = ToModel<Dependent, DependentDomainModel>(domainModel);

				_repository.Setup(x => x.InsertAsync(It.IsAny<Dependent>())).ReturnsAsync(model);

				var result = await _domain.InsertAsync(new DependentDomainModel()).ConfigureAwait(false);
				result.Should().BeOfType<DependentDomainModel>();
				result.Should().BeEquivalentTo(domainModel);
			}
		#endregion

		#region UPDATE
			[Fact]
			public void UpdateAsync_WithNullDependentParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _domain.UpdateAsync(null);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void UpdateAsync_GivenDependentDoesNotExist_ReturnNullDependent()
			{
				_repository.Setup(x => x.UpdateAsync(It.IsAny<Dependent>())).ReturnsAsync((Dependent)null);

				var result = await _domain.UpdateAsync(new DependentDomainModel()).ConfigureAwait(false);
				result.Should().BeNull();
			}

			[Fact]
			public async void UpdateAsync_GivenDependentExist_ReturnUpdatedDependent()
			{
				var domainModel = CreateDomain();
				var model = ToModel<Dependent, DependentDomainModel>(domainModel);
				model.FirstName = Guid.NewGuid().ToString();
				model.LastName = Guid.NewGuid().ToString();

				_repository.Setup(x => x.UpdateAsync(It.IsAny<Dependent>())).ReturnsAsync(model);

				var result = await _domain.UpdateAsync(new DependentDomainModel()).ConfigureAwait(false);

				AssertionOptions.AssertEquivalencyUsing(options => options.ExcludingMissingMembers());
				result.Should().BeOfType<DependentDomainModel>();
				result.Should().NotBe(domainModel);
				result.Should().BeEquivalentTo(model);
			}
		#endregion

		private DependentDomainModel CreateDomain()
		{
			var domainModel = new Faker<DependentDomainModel>()
				.RuleFor(fake => fake.FirstName, rule => rule.Person.FirstName)
				.RuleFor(fake => fake.LastName, rule => rule.Person.LastName)
				.RuleFor(fake => fake.ModifiedDate, rule => DateTime.UtcNow);

			return domainModel.Generate();
		}
	}
}
