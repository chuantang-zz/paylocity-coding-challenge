namespace Paylocity.Api.Domain.Interface
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Model;
	using Repository.Interface;
	using Repository.Model;

	public class EmployeeDomain : BaseDomain, IEmployeeDomain
	{
		private bool _disposed;
		private IEmployeeRepository _repository;
		private IDependentRepository _repositoryDependent;

		public EmployeeDomain(IEmployeeRepository repository, IDependentRepository repositoryDependent)
		{
			_repository = repository;
			_repositoryDependent = repositoryDependent;
		}

		/// <summary> <see cref="IEmployeeDomain.DeleteAsync"/> </summary>
		/// <exception cref="ArgumentNullException"><paramref name="domainModel"/> is <see langword="null"/></exception>
		public async Task<EmployeeDomainModel> DeleteAsync(Guid id)
		{
			if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

			var deleted = await _repository.DeleteAsync(id).ConfigureAwait(false);
			var deletedDomainModel = ToModel<EmployeeDomainModel, Employee>(deleted);

			return deletedDomainModel;
		}

		/// <summary> <see cref="IEmployeeDomain.GetAsync()"/> </summary>
		public async Task<IList<EmployeeDomainModel>> GetAsync()
		{
			var models = await _repository.GetAsync().ConfigureAwait(false);
			IList<EmployeeDomainModel> domainModels = ToListModel<EmployeeDomainModel, Employee>(models);

			foreach (var model in domainModels)
			{
				CalculateDiscount(model);

				model.Dependents = await GetDependents(model.EmployeeId);
				model.DependentCount = model.Dependents.Count();
			}

			return domainModels;
		}

		/// <summary> <see cref="IEmployeeDomain.GetAsync(Guid)"/> </summary>
		public async Task<EmployeeDomainModel> GetAsync(Guid id)
		{
			if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

			var model = await _repository.GetAsync(id).ConfigureAwait(false);
			var domainModel = ToModel<EmployeeDomainModel, Employee>(model);

			domainModel.Dependents = await GetDependents(model.EmployeeId);
			domainModel.DependentCount = domainModel.Dependents.Count();

			return domainModel;
		}

		/// <summary> <see cref="IEmployeeDomain.InsertAsync"/> </summary>
		/// <exception cref="ArgumentNullException"><paramref name="domainModel"/> is <see langword="null"/></exception>
		public async Task<EmployeeDomainModel> InsertAsync(EmployeeDomainModel domainModel)
		{
			if (domainModel == null) throw new ArgumentNullException(nameof(domainModel));

			var model = ToModel<Employee, EmployeeDomainModel>(domainModel);
			model = await _repository.InsertAsync(model).ConfigureAwait(false);
			domainModel = ToModel<EmployeeDomainModel, Employee>(model);

			return domainModel;
		}

		/// <summary> <see cref="IEmployeeDomain.UpdateAsync"/> </summary>
		/// <exception cref="ArgumentNullException"><paramref name="domainModel"/> is <see langword="null"/></exception>
		public async Task<EmployeeDomainModel> UpdateAsync(EmployeeDomainModel domainModel)
		{
			if (domainModel == null) throw new ArgumentNullException(nameof(domainModel));

			var model = ToModel<Employee, EmployeeDomainModel>(domainModel);
			model = await _repository.UpdateAsync(model).ConfigureAwait(false);
			domainModel = ToModel<EmployeeDomainModel, Employee>(model);

			return domainModel;
		}

		private async Task<IEnumerable<DependentDomainModel>> GetDependents(Guid id)
		{
			var models = await _repositoryDependent.GetByEmployeeIdAsync(id).ConfigureAwait(false);
			var domainModels = ToListModel<DependentDomainModel, Dependent>(models);

			return domainModels;
		}

		private void CalculateDiscount(EmployeeDomainModel model)
		{
			model.YearlyWage = 2000 * 26;

			model.TotalDeductions = 1000;
			var applyDiscount = model.FirstName.StartsWith('A');
			if (applyDiscount)
			{
				var discount = model.YearlyWage / 10;
				model.TotalDeductions = model.TotalDeductions + discount;
			}

			model.FinalWage = model.YearlyWage - model.TotalDeductions;
		}

		~EmployeeDomain() { Dispose(false); }

		#region IDISPOSABLE IMPLEMENTATION
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
			protected virtual void Dispose(bool disposing)
			{
				if (_disposed) return;

				if (disposing)
				{
					// free other managed objects that implement IDisposable only
					_repository.Dispose();
					_repositoryDependent.Dispose();
				}

				// release any unmanaged objects set the object references to null
				_repository = null;
				_repositoryDependent = null;

				_disposed = true;
			}
		#endregion
	}
}
