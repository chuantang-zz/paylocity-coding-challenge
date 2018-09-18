namespace Paylocity.Api.Repository
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Interface;
	using Microsoft.EntityFrameworkCore;
	using Model;

	/// <see cref="IDependentRepository"/>
	public class DependentRepository : IDependentRepository
	{
		private bool _disposed;
		private PaylocityContext _context;

		/// <summary> This is the constructor of the repository that the DI will set all of the Interfaces </summary>
		/// <param name="context"> The context that is used to access the database </param>
		public DependentRepository(PaylocityContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));

			context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		/// <summary> <see cref="IDependentRepository.DeleteAsync"/> </summary>
		/// <param name="id"> The id of the <see cref="Dependent"/> that is to be deleted </param>
		/// <returns> The <see cref="Dependent"/> was deleted </returns>
		public async Task<Dependent> DeleteAsync(Guid id)
		{
			if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

			var selected = await GetAsync(id).ConfigureAwait(false);
			if (selected != null)
			{
				_context.Dependents.Remove(selected);
				await _context.SaveChangesAsync().ConfigureAwait(false);

				return selected;
			}

			return null;
		}

		/// <summary> <see cref="IDependentRepository.GetAsync()"/> </summary>
		/// <returns> A list of <see cref="Dependent"/> </returns>
		public async Task<IList<Dependent>> GetAsync()
		{
			List<Dependent> models = await _context.Dependents.ToListAsync().ConfigureAwait(false);

			return models;
		}

		/// <summary> <see cref="IDependentRepository.GetAsync(Guid)"/> </summary>
		/// <param name="id"> This is the primary key of the <see cref="Dependent"/> </param>
		/// <returns> The found <see cref="Dependent"/> </returns>
		public async Task<Dependent> GetAsync(Guid id)
		{
			if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

			Dependent model = await _context.Dependents.FindAsync(id).ConfigureAwait(false);

			return model;
		}

		/// <summary> <see cref="IDependentRepository.GetByEmployeeIdAsync(Guid)"/> </summary>
		/// <returns> A list of <see cref="Dependent"/> </returns>
		public async Task<IList<Dependent>> GetByEmployeeIdAsync(Guid id)
		{
			List<Dependent> models = await _context.Dependents.Where(dependent => dependent.EmployeeId == id).ToListAsync().ConfigureAwait(false);

			return models;
		}

		/// <summary> <see cref="IDependentRepository.InsertAsync"/> </summary>
		/// <param name="model"> The <see cref="Dependent"/> to be inserted </param>
		/// <returns> The inserted <see cref="Dependent"/> </returns>
		public async Task<Dependent> InsertAsync(Dependent model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			_context.Dependents.Add(model);

			int returnedCount = await _context.SaveChangesAsync().ConfigureAwait(false);
			if (returnedCount <= 0) model = null;

			return model;
		}

		/// <summary> <see cref="IDependentRepository.UpdateAsync"/> </summary>
		/// <param name="model"> The <see cref="Dependent"/> to be updated </param>
		/// <returns> The updated <see cref="Dependent"/> </returns>
		public async Task<Dependent> UpdateAsync(Dependent model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			Dependent updated = await GetAsync(model.DependentId).ConfigureAwait(false);
			if (updated == null) return null;

			model.ModifiedDate = DateTime.UtcNow;

			_context.Dependents.Update(model);
			await _context.SaveChangesAsync().ConfigureAwait(false);

			return updated;
		}

		~DependentRepository() { Dispose(false); }

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
					_context.Dispose();
				}

				// release any unmanaged objects set the object references to null

				_disposed = true;
			}
		#endregion
	}
}
