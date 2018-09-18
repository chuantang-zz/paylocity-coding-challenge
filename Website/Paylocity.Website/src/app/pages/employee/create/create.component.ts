import { Component, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AppSettings } from '../../../app.settings';
import { Settings } from '../../../app.settings.model';

import { Employee } from '../employee.model';
import { EmployeeService } from '../employee.service';

@Component
(
	{
		selector: 'app-employee-create',
		templateUrl: './create.component.html'
	}
)

export class EmployeeCreateComponent implements AfterViewInit
{
	public formCreate: FormGroup;
	public settings: Settings;

	public employee: Employee = new Employee();

	public errorMessage: string = '';

	constructor(private router: Router, public appSettings: AppSettings, public formBuilder: FormBuilder, private employeeService: EmployeeService)
	{
		this.settings = this.appSettings.settings;
		this.formCreate = this.formBuilder.group
		(
			{
				firstName: [this.employee.firstName, Validators.compose([Validators.required])],
				lastName: [this.employee.lastName, Validators.compose([Validators.required])]
			}
		);
	}

	public onSubmit(values: Object): void
	{
		this.errorMessage = '';

		if (this.formCreate.valid)
		{
			this.employee.firstName = this.formCreate.value.firstName;
			this.employee.lastName = this.formCreate.value.lastName;

			this.employeeService
				.create(this.employee)
				.subscribe
				(
					response => this.router.navigate(['/employee']),
					error =>
					{
						console.error(error);
						this.errorMessage = error.error;
					}
				);
		}
	}

	ngAfterViewInit() { this.settings.loadingSpinner = false; }
}
