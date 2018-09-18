namespace Paylocity.Api.Repository.Model
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table(name: "Employees", Schema = "dbo")]
	public class Employee
	{
		[Column(name: nameof(EmployeeId), Order = 0, TypeName = "uniqueidentifier")]
		[Key]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Employee Id is a required field")]
		public Guid EmployeeId { get; set; }

		[Column(name: nameof(FirstName), Order = 1, TypeName = "nvarchar(256)")]
		[Required(AllowEmptyStrings = true, ErrorMessage = "First Name is a required field")]
		[StringLength(maximumLength: 256, ErrorMessage = "Value has a maximum length of 256 characters")]
		public string FirstName { get; set; }

		[Column(name: nameof(LastName), Order = 2, TypeName = "nvarchar(256)")]
		[Required(AllowEmptyStrings = true, ErrorMessage = "Last Name is a required field")]
		[StringLength(maximumLength: 256, ErrorMessage = "Value has a maximum length of 256 characters")]
		public string LastName { get; set; }

		[Column(name: nameof(ModifiedDate), Order = 3, TypeName = "datetime2")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Modified Date is a required field")]
		public DateTime ModifiedDate { get; set; }
	}
}
