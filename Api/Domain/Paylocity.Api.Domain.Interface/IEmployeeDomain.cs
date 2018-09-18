namespace Paylocity.Api.Domain.Interface
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Model;

	public interface IEmployeeDomain : IDisposable
	{
		Task<EmployeeDomainModel> DeleteAsync(Guid id);

		Task<IList<EmployeeDomainModel>> GetAsync();
		Task<EmployeeDomainModel> GetAsync(Guid id);

		Task<EmployeeDomainModel> InsertAsync(EmployeeDomainModel model);

		Task<EmployeeDomainModel> UpdateAsync(EmployeeDomainModel model);
	}
}
