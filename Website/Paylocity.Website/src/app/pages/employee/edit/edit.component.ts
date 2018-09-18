import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { AppSettings } from '../../../app.settings';
import { Settings } from '../../../app.settings.model';

import { Employee } from '../employee.model';
import { EmployeeService } from '../employee.service';

@Component
(
	{
		selector: 'app-employee-edit',
		templateUrl: './edit.component.html'
	}
)

export class EmployeeEditComponent implements AfterViewInit, OnInit
{
	public formCreate: FormGroup;
	public settings: Settings;

	public employee: Employee = new Employee();

	public errorMessage: string = '';

	constructor(private router: Router, public appSettings: AppSettings, private activatedRoute: ActivatedRoute, public formBuilder: FormBuilder, private employeeService: EmployeeService)
	{
		this.settings = this.appSettings.settings;
		this.formCreate = this.formBuilder.group
		(
			{
				firstName: [null, Validators.compose([Validators.required])],
				lastName: [null, Validators.compose([Validators.required])],
			}
		);
	}

	ngOnInit(): void
	{
		this.activatedRoute.params.forEach
		(
			(params: Params) =>
			{
				if (params['id'] !== undefined) { this.employee.employeeId = params['id']; }
			}
		);

		this.getDetail();
	}

	getDetail()
	{
		this.employeeService
			.getByEmployeeId(this.employee.employeeId)
			.subscribe
			(
				result =>
				{
					this.employee = result;

					this.formCreate.get('firstName').setValue(this.employee.firstName);
					this.formCreate.get('lastName').setValue(this.employee.lastName);
				},
				error => console.error(error)
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
				.update(this.employee)
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

	cancelDetail() { this.router.navigate(['/employee']); }

	ngAfterViewInit() { this.settings.loadingSpinner = false; }
}
