namespace Paylocity.Api.Repository.Test.Integration
{
	using System;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Interface;
	using Model;
	using Xunit;

	[Collection("Do Not Run Parallel")]
	[Trait("Repository", "Dependent")]
	public class DependentRepositoryFixture : BaseRepositoryFixture
	{
		private readonly IDependentRepository _repository;
		private readonly DependentBuilder _builder;
		private readonly DependentHelper _helper;

		private Employee _employee = new Employee();

		public DependentRepositoryFixture()
		{
			_repository = new DependentRepository(Context);
			_builder = new DependentBuilder();
			_helper = new DependentHelper(Context);
		}

		#region DELETE
			[Fact]
			public void DeleteAsync_WithNullDependentParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _repository.DeleteAsync(Guid.Empty);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void DeleteAsync_GivenDependentExist_Delete_ReturnDependent()
			{
				var defaultData = await BuildDefaultAsync();
				await _helper.AddAsync(defaultData).ConfigureAwait(false);

				try
				{
					var deleted = await _repository.DeleteAsync(defaultData.DependentId).ConfigureAwait(false);
					deleted.Should().NotBeNull();
					deleted.Should().BeOfType<Dependent>();
					deleted.Should().Be(defaultData);
				}
				finally { await CleanAsync(defaultData).ConfigureAwait(false); }
			}
		#endregion

		#region GET
			[Fact]
			public async void GetAsync_ReturnAll()
			{
				var defaultData = await BuildDefaultAsync();
				await _helper.AddAsync(defaultData).ConfigureAwait(false);

				try
				{
					var selected = await _repository.GetAsync().ConfigureAwait(false);
					selected.Should().NotBeNull();
					selected.Should().NotBeEmpty();
				}
				finally { await CleanAsync(defaultData).ConfigureAwait(false); }
			}
		#endregion

		#region GET BY ID
			[Fact]
			public void GetAsync_ById_WithDefaultGuidDependentIdParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _repository.GetAsync(Guid.Empty);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void GetAsync_GivenDependentIdExist_ReturnDependent()
			{
				var defaultData = await BuildDefaultAsync();
				await _helper.AddAsync(defaultData).ConfigureAwait(false);

				try
				{
					var selected = await _repository.GetAsync(defaultData.DependentId).ConfigureAwait(false);
					selected.Should().NotBeNull();
					selected.Should().BeOfType<Dependent>();
					selected.Should().Be(defaultData);
				}
				finally { await CleanAsync(defaultData).ConfigureAwait(false); }
			}
		#endregion

		#region INSERT
			[Fact]
			public void InsertAsync_WithNullDependentParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _repository.InsertAsync(null);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void InsertAsync_GivenDependentDoesNotExist_ReturnInsertedDependent()
			{
				var defaultData = await BuildDefaultAsync();

				try
				{
					var inserted = await _repository.InsertAsync(defaultData).ConfigureAwait(false);
					inserted.Should().Be(defaultData);
				}
				finally { await CleanAsync(defaultData).ConfigureAwait(false); }
			}
		#endregion

		#region UPDATE
			[Fact]
			public void UpdateAsync_WithNullDependentParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _repository.UpdateAsync(null);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void UpdateAsync_GivenDependentDoesNotExist_ReturnNullDependent()
			{
				var defaultData = await BuildDefaultAsync();
				defaultData.DependentId = Guid.NewGuid();

				try
				{
					var updated = await _repository.UpdateAsync(defaultData).ConfigureAwait(false);
					updated.Should().BeNull();
				}
				finally { await CleanAsync(defaultData).ConfigureAwait(false); }
			}

			[Fact]
			public async void UpdateAsync_GivenDependentExist_ReturnUpdatedDependent()
			{
				var defaultData = await BuildDefaultAsync();
				await _helper.AddAsync(defaultData).ConfigureAwait(false);
				defaultData.FirstName = Guid.NewGuid().ToString();
				defaultData.LastName = Guid.NewGuid().ToString();

				try
				{
					var updated = await _repository.UpdateAsync(defaultData).ConfigureAwait(false);
					updated.Should().NotBeNull();
					updated.Should().BeOfType<Dependent>();
					updated.Should().Be(defaultData);
				}
				finally { await CleanAsync(defaultData).ConfigureAwait(false); }
			}
		#endregion

		private async Task<Dependent> BuildDefaultAsync()
		{
			EmployeeBuilder employeeBuilder = new EmployeeBuilder();
			_employee = employeeBuilder.BuildDefault();
			EmployeeHelper employeeHelper = new EmployeeHelper(Context);
			await employeeHelper.AddAsync(_employee).ConfigureAwait(false);

			var defaultData = _builder.BuildDefault();
			defaultData.EmployeeId = _employee.EmployeeId;

			return defaultData;
		}
		private async Task CleanAsync(Dependent model)
		{
			await _helper.DeleteAsync(model).ConfigureAwait(false);

			EmployeeHelper employeeHelper = new EmployeeHelper(Context);
			await employeeHelper.DeleteAsync(_employee).ConfigureAwait(false);
		}
	}
}
