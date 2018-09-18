namespace Paylocity.Api.Domain.Model
{
	using System;

	public class DependentDomainModel
	{
		public Guid DependentId { get; set; }

		public Guid EmployeeId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime ModifiedDate { get; set; }
	}
}
