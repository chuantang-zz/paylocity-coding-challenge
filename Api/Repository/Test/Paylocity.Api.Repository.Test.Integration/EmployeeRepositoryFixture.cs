namespace Paylocity.Api.Repository.Test.Integration
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Interface;
	using Model;
	using Xunit;

	[Collection("Do Not Run Parallel")]
	[Trait("Repository", "Employee")]
	public class EmployeeRepositoryFixture : BaseRepositoryFixture
	{
		private readonly IEmployeeRepository _repository;
		private readonly EmployeeBuilder _builder;
		private readonly EmployeeHelper _helper;

		public EmployeeRepositoryFixture()
		{
			_repository = new EmployeeRepository(Context);
			_builder = new EmployeeBuilder();
			_helper = new EmployeeHelper(Context);
		}

		#region DELETE
			[Fact]
			public void DeleteAsync_WithNullEmployeeParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _repository.DeleteAsync(Guid.Empty);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void DeleteAsync_GivenEmployeeExist_Delete_ReturnEmployee()
			{
				var defaultData = BuildDefaultAsync();
				await _helper.AddAsync(defaultData).ConfigureAwait(false);

				try
				{
					var deleted = await _repository.DeleteAsync(defaultData.EmployeeId).ConfigureAwait(false);
					deleted.Should().NotBeNull();
					deleted.Should().BeOfType<Employee>();
					deleted.Should().Be(defaultData);
				}
				finally { await CleanAsync(defaultData).ConfigureAwait(false); }
			}
		#endregion

		#region GET
			[Fact]
			public async void GetAsync_ReturnAll()
			{
				var defaultData = BuildDefaultAsync();
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
			public void GetAsync_ById_WithDefaultGuidEmployeeIdParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _repository.GetAsync(Guid.Empty);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void GetAsync_GivenEmployeeIdExist_ReturnEmployee()
			{
				var defaultData = BuildDefaultAsync();
				await _helper.AddAsync(defaultData).ConfigureAwait(false);

				try
				{
					var selected = await _repository.GetAsync(defaultData.EmployeeId).ConfigureAwait(false);
					selected.Should().NotBeNull();
					selected.Should().BeOfType<Employee>();
					selected.Should().Be(defaultData);
				}
				finally { await CleanAsync(defaultData).ConfigureAwait(false); }
			}
		#endregion

		#region INSERT
			[Fact]
			public void InsertAsync_WithNullEmployeeParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _repository.InsertAsync(null);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void InsertAsync_GivenEmployeeDoesNotExist_ReturnInsertedEmployee()
			{
				var defaultData = BuildDefaultAsync();

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
			public void UpdateAsync_WithNullEmployeeParameter_ReturnArgumentNullException()
			{
				Func<Task> act = () => _repository.UpdateAsync(null);
				act.Should().Throw<ArgumentNullException>();
			}

			[Fact]
			public async void UpdateAsync_GivenEmployeeDoesNotExist_ReturnNullEmployee()
			{
				var defaultData = BuildDefaultAsync();
				defaultData.EmployeeId = Guid.NewGuid();

				try
				{
					var updated = await _repository.UpdateAsync(defaultData).ConfigureAwait(false);
					updated.Should().BeNull();
				}
				finally { await CleanAsync(defaultData).ConfigureAwait(false); }
			}

			[Fact]
			public async void UpdateAsync_GivenEmployeeExist_ReturnUpdatedEmployee()
			{
				var defaultData = BuildDefaultAsync();
				await _helper.AddAsync(defaultData).ConfigureAwait(false);
				defaultData.FirstName = Guid.NewGuid().ToString();
				defaultData.LastName = Guid.NewGuid().ToString();

				try
				{
					var updated = await _repository.UpdateAsync(defaultData).ConfigureAwait(false);
					updated.Should().NotBeNull();
					updated.Should().BeOfType<Employee>();
					updated.Should().Be(defaultData);
				}
				finally { await CleanAsync(defaultData).ConfigureAwait(false); }
			}
		#endregion

		private Employee BuildDefaultAsync()
		{
			var defaultData = _builder.BuildDefault();

			return defaultData;
		}
		private async Task CleanAsync(Employee model) { await _helper.DeleteAsync(model).ConfigureAwait(false); }
	}
}
