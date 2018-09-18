namespace Paylocity.Api.Repository.Test.Integration
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;
	using Model;
	using Repository;

	public class EmployeeHelper
	{
		private readonly PaylocityContext _context;
		public EmployeeHelper(PaylocityContext context) { _context = context; }

		public async Task AddAsync(Employee model)
		{
			_context.Employees.Add(model);

			await _context.SaveChangesAsync().ConfigureAwait(false);
		}
		public async Task DeleteAsync(Employee model)
		{
			var exist = await GetAsync(model.EmployeeId).ConfigureAwait(false);
			if (exist != null) _context.Employees.Remove(model);

			await _context.SaveChangesAsync().ConfigureAwait(false);
		}
		private async Task<Employee> GetAsync(Guid id)
		{
			var model = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id).ConfigureAwait(false);

			return model;
		}
	}
}
