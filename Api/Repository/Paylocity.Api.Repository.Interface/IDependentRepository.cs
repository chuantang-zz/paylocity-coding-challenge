namespace Paylocity.Api.Repository.Interface
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Model;

	public interface IDependentRepository : IDisposable
	{
		Task<Dependent> DeleteAsync(Guid id);

		Task<IList<Dependent>> GetAsync();
		Task<Dependent> GetAsync(Guid id);
		Task<IList<Dependent>> GetByEmployeeIdAsync(Guid id);

		Task<Dependent> InsertAsync(Dependent model);

		Task<Dependent> UpdateAsync(Dependent model);
	}
}
