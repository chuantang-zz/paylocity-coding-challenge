namespace Paylocity.Api.Domain.Model
{
	using System;
	using System.Collections.Generic;

	public class EmployeeDomainModel
	{
		public Guid EmployeeId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime ModifiedDate { get; set; }

		public IEnumerable<DependentDomainModel> Dependents { get; set; }

		public int DependentCount { get; set; }

		public int YearlyWage { get; set; }

		public int TotalDeductions { get; set; }

		public int FinalWage { get; set; }

		public EmployeeDomainModel()
		{
			Dependents = new List<DependentDomainModel>();
		}
	}
}
