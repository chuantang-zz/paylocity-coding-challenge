import { Component, OnInit, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { AppSettings } from '../../../app.settings';
import { Settings } from '../../../app.settings.model';

import { Dependent } from '../dependent.model';
import { DependentService } from '../dependent.service';

@Component
(
	{
		selector: 'app-dependent-create',
		templateUrl: './create.component.html'
	}
)

export class DependentCreateComponent implements AfterViewInit, OnInit
{
	public formCreate: FormGroup;
	public settings: Settings;

	public dependent: Dependent = new Dependent();

	public errorMessage: string = '';

	constructor(private router: Router, public appSettings: AppSettings, private activatedRoute: ActivatedRoute, public formBuilder: FormBuilder, private dependentService: DependentService)
	{
		this.settings = this.appSettings.settings;
		this.formCreate = this.formBuilder.group
		(
			{
				firstName: [this.dependent.firstName, Validators.compose([Validators.required])],
				lastName: [this.dependent.lastName, Validators.compose([Validators.required])]
			}
		);
	}

	ngOnInit(): void
	{
		this.activatedRoute.params.forEach
		(
			(params: Params) =>
			{
				if (params['id'] !== undefined) { this.dependent.employeeId = params['id']; }
			}
		);
	}

	public onSubmit(values: Object): void
	{
		this.errorMessage = '';

		if (this.formCreate.valid)
		{
			this.dependent.firstName = this.formCreate.value.firstName;
			this.dependent.lastName = this.formCreate.value.lastName;

			this.dependentService
				.create(this.dependent)
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
