namespace Paylocity.Api.Repository.Interface
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Model;

	public interface IEmployeeRepository : IDisposable
	{
		Task<Employee> DeleteAsync(Guid id);

		Task<IList<Employee>> GetAsync();
		Task<Employee> GetAsync(Guid id);

		Task<Employee> InsertAsync(Employee model);

		Task<Employee> UpdateAsync(Employee model);
	}
}
