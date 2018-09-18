export class Employee
{
	public employeeId: string;
	public firstName: string;
	public lastName: string;
	public modifiedDate: string;
	public dependents: [];

	public dependentCount: number;

	public yearlyWage: number;
	public totalDeductions: number;
	public finalWage: number;

	constructor()
	{
		this.employeeId = '00000000-0000-0000-0000-000000000000';
		this.firstName = '';
		this.lastName = '';
		this.modifiedDate = new Date().toLocaleDateString();
		this.dependents = [];

		this.dependentCount = 0;
		this.yearlyWage = 0;
		this.totalDeductions = 0;
		this.finalWage = 0;
	}
}
