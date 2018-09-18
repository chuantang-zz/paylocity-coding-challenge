import { Component, ViewChild } from '@angular/core';
import { DatatableComponent } from '@swimlane/ngx-datatable';
import { Router } from '@angular/router';

import { AppSettings } from '../../app.settings';
import { Settings } from '../../app.settings.model';

// models type
import { Employee } from './employee.model';

// service
import { EmployeeService } from './employee.service';

@Component
(
	{
		selector: 'app-employee',
		templateUrl: './employee.component.html'
	}
)

export class EmployeeComponent
{
	@ViewChild(DatatableComponent) table: DatatableComponent;
	editing = {};
	rows = [];
	temp = [];
	selected = [];
	loadingIndicator: boolean = true;
	reorderable: boolean = true;

	public settings: Settings;
	public employees: Employee[] = [];

	constructor(private router: Router, public appSettings: AppSettings, private employeeService: EmployeeService)
	{
		this.settings = this.appSettings.settings;
		this.getData();
	}

	routeToCreate()
	{
		this.router.navigate(['/employee/create']);
	}

	updateFilter(event)
	{
		const val = event.target.value.toLowerCase();
		const temp = this.temp.filter(function (d) { return d.firstName.toLowerCase().indexOf(val) !== -1 || !val; });

		this.rows = temp;
		this.table.offset = 0;
	}

	toggleExpandRow(row) { this.table.rowDetail.toggleExpandRow(row); }

	onDetailToggle(event) { }

	getData()
	{
		this.employeeService
			.get()
			.subscribe
			(
				results =>
				{
					this.employees = results;

					this.temp = [...this.employees];
					this.rows = this.employees;
					setTimeout(() => { this.loadingIndicator = false; }, 1500);
				},
				error => console.error(error)
			);
	}

	delete(employeeId: string)
	{
		this.employeeService
			.delete(employeeId)
			.subscribe
			(
				result => this.getData(),
				error => console.error(error)
			);
	}

	goToDetail(id: string) { this.router.navigate(['/employee/edit/' + id]); }

	goToCreateDependent(id: string) { this.router.navigate(['/dependent/create/' + id]); }
}
