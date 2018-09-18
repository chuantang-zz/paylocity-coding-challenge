namespace Paylocity.Api.Repository
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Interface;
	using Microsoft.EntityFrameworkCore;
	using Model;

	/// <see cref="IEmployeeRepository"/>
	public class EmployeeRepository : IEmployeeRepository
	{
		private bool _disposed;
		private PaylocityContext _context;

		/// <summary> This is the constructor of the repository that the DI will set all of the Interfaces </summary>
		/// <param name="context"> The context that is used to access the database </param>
		public EmployeeRepository(PaylocityContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));

			context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		/// <summary> <see cref="IEmployeeRepository.DeleteAsync"/> </summary>
		/// <param name="id"> The id of the <see cref="Employee"/> that is to be deleted </param>
		/// <returns> The <see cref="Employee"/> was deleted </returns>
		public async Task<Employee> DeleteAsync(Guid id)
		{
			if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

			var selected = await GetAsync(id).ConfigureAwait(false);
			if (selected != null)
			{
				_context.Employees.Remove(selected);
				await _context.SaveChangesAsync().ConfigureAwait(false);

				return selected;
			}

			return null;
		}

		/// <summary> <see cref="IEmployeeRepository.GetAsync()"/> </summary>
		/// <returns> A list of <see cref="Employee"/> </returns>
		public async Task<IList<Employee>> GetAsync()
		{
			List<Employee> models = await _context.Employees.ToListAsync().ConfigureAwait(false);

			return models;
		}

		/// <summary> <see cref="IEmployeeRepository.GetAsync(Guid)"/> </summary>
		/// <param name="id"> This is the primary key of the <see cref="Employee"/> </param>
		/// <returns> The found <see cref="Employee"/> </returns>
		public async Task<Employee> GetAsync(Guid id)
		{
			if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

			Employee model = await _context.Employees.FindAsync(id).ConfigureAwait(false);

			return model;
		}

		/// <summary> <see cref="IEmployeeRepository.InsertAsync"/> </summary>
		/// <param name="model"> The <see cref="Employee"/> to be inserted </param>
		/// <returns> The inserted <see cref="Employee"/> </returns>
		public async Task<Employee> InsertAsync(Employee model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			_context.Employees.Add(model);

			int returnedCount = await _context.SaveChangesAsync().ConfigureAwait(false);
			if (returnedCount <= 0) model = null;

			return model;
		}

		/// <summary> <see cref="IEmployeeRepository.UpdateAsync"/> </summary>
		/// <param name="model"> The <see cref="Employee"/> to be updated </param>
		/// <returns> The updated <see cref="Employee"/> </returns>
		public async Task<Employee> UpdateAsync(Employee model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			Employee updated = await GetAsync(model.EmployeeId).ConfigureAwait(false);
			if (updated == null) return null;

			model.ModifiedDate = DateTime.UtcNow;

			_context.Employees.Update(model);
			await _context.SaveChangesAsync().ConfigureAwait(false);

			return updated;
		}

		~EmployeeRepository() { Dispose(false); }

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
