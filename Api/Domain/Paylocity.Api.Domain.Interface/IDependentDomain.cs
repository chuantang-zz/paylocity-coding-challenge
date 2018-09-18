namespace Paylocity.Api.Domain.Interface
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Model;

	public interface IDependentDomain : IDisposable
	{
		Task<DependentDomainModel> DeleteAsync(Guid id);

		Task<IList<DependentDomainModel>> GetAsync();
		Task<DependentDomainModel> GetAsync(Guid id);
		Task<IList<DependentDomainModel>> GetByEmployeeIdAsync(Guid id);

		Task<DependentDomainModel> InsertAsync(DependentDomainModel model);

		Task<DependentDomainModel> UpdateAsync(DependentDomainModel model);
	}
}
