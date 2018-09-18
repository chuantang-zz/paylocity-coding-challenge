export class Dependent
{
	public dependentId: string;
	public employeeId: string;
	public firstName: string;
	public lastName: string;
	public modifiedDate: string;

	constructor()
	{
		this.dependentId = '00000000-0000-0000-0000-000000000000';
		this.employeeId = '00000000-0000-0000-0000-000000000000';
		this.firstName = '';
		this.lastName = '';
		this.modifiedDate = new Date().toLocaleDateString();
	}
}
