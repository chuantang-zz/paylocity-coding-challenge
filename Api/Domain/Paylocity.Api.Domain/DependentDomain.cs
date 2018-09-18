namespace Paylocity.Api.Domain.Interface
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Model;
	using Repository.Interface;
	using Repository.Model;

	public class DependentDomain : BaseDomain, IDependentDomain
	{
		private bool _disposed;
		private IDependentRepository _repository;

		public DependentDomain(IDependentRepository repository) { _repository = repository; }

		/// <summary> <see cref="IDependentDomain.DeleteAsync"/> </summary>
		/// <exception cref="ArgumentNullException"><paramref name="domainModel"/> is <see langword="null"/></exception>
		public async Task<DependentDomainModel> DeleteAsync(Guid id)
		{
			if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

			var deleted = await _repository.DeleteAsync(id).ConfigureAwait(false);
			var deletedDomainModel = ToModel<DependentDomainModel, Dependent>(deleted);

			return deletedDomainModel;
		}

		/// <summary> <see cref="IDependentDomain.GetAsync()"/> </summary>
		public async Task<IList<DependentDomainModel>> GetAsync()
		{
			var models = await _repository.GetAsync().ConfigureAwait(false);
			IList<DependentDomainModel> domainModels = ToListModel<DependentDomainModel, Dependent>(models);

			return domainModels;
		}
		/// <summary> <see cref="IDependentDomain.GetAsync(Guid)"/> </summary>
		public async Task<DependentDomainModel> GetAsync(Guid id)
		{
			if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

			var model = await _repository.GetAsync(id).ConfigureAwait(false);
			var domainModel = ToModel<DependentDomainModel, Dependent>(model);

			return domainModel;
		}
		/// <summary> <see cref="IDependentDomain.GetByCategoryIdAsync(Guid)"/> </summary>
		public async Task<IList<DependentDomainModel>> GetByEmployeeIdAsync(Guid id)
		{
			var models = await _repository.GetByEmployeeIdAsync(id).ConfigureAwait(false);
			IList<DependentDomainModel> domainModels = ToListModel<DependentDomainModel, Dependent>(models);

			return domainModels;
		}

		/// <summary> <see cref="IDependentDomain.InsertAsync"/> </summary>
		/// <exception cref="ArgumentNullException"><paramref name="domainModel"/> is <see langword="null"/></exception>
		public async Task<DependentDomainModel> InsertAsync(DependentDomainModel domainModel)
		{
			if (domainModel == null) throw new ArgumentNullException(nameof(domainModel));

			var model = ToModel<Dependent, DependentDomainModel>(domainModel);
			model = await _repository.InsertAsync(model).ConfigureAwait(false);
			domainModel = ToModel<DependentDomainModel, Dependent>(model);

			return domainModel;
		}

		/// <summary> <see cref="IDependentDomain.UpdateAsync"/> </summary>
		/// <exception cref="ArgumentNullException"><paramref name="domainModel"/> is <see langword="null"/></exception>
		public async Task<DependentDomainModel> UpdateAsync(DependentDomainModel domainModel)
		{
			if (domainModel == null) throw new ArgumentNullException(nameof(domainModel));

			var model = ToModel<Dependent, DependentDomainModel>(domainModel);
			model = await _repository.UpdateAsync(model).ConfigureAwait(false);
			domainModel = ToModel<DependentDomainModel, Dependent>(model);

			return domainModel;
		}

		~DependentDomain() { Dispose(false); }

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
				}

				// release any unmanaged objects set the object references to null
				_repository = null;

				_disposed = true;
			}
		#endregion
	}
}
