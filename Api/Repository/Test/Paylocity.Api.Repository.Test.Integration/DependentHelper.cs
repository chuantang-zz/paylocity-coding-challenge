namespace Paylocity.Api.Repository.Test.Integration
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;
	using Model;
	using Repository;

	public class DependentHelper
	{
		private readonly PaylocityContext _context;
		public DependentHelper(PaylocityContext context) { _context = context; }

		public async Task AddAsync(Dependent model)
		{
			_context.Dependents.Add(model);

			await _context.SaveChangesAsync().ConfigureAwait(false);
		}
		public async Task DeleteAsync(Dependent model)
		{
			var exist = await GetAsync(model.DependentId).ConfigureAwait(false);
			if (exist != null) _context.Dependents.Remove(model);

			await _context.SaveChangesAsync().ConfigureAwait(false);
		}
		private async Task<Dependent> GetAsync(Guid id)
		{
			var model = await _context.Dependents.FirstOrDefaultAsync(x => x.DependentId == id).ConfigureAwait(false);

			return model;
		}
	}
}
