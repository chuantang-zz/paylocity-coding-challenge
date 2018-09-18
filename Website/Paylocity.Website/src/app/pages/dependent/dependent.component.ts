import { Component, OnInit, ViewChild } from '@angular/core';
import { DatatableComponent } from '@swimlane/ngx-datatable';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { AppSettings } from '../../app.settings';
import { Settings } from '../../app.settings.model';

// models type
import { Dependent } from './dependent.model';

// service
import { DependentService } from './dependent.service';

@Component
(
	{
		selector: 'app-dependent',
		templateUrl: './dependent.component.html'
	}
)

export class DependentComponent implements OnInit
{
	@ViewChild(DatatableComponent) table: DatatableComponent;
	editing = {};
	rows = [];
	temp = [];
	selected = [];
	loadingIndicator: boolean = true;
	reorderable: boolean = true;

	public settings: Settings;
	public dependents: Dependent[] = [];
	public employeeId: string = '';

	constructor(private router: Router, public appSettings: AppSettings, private activatedRoute: ActivatedRoute, private dependentService: DependentService)
	{
		this.settings = this.appSettings.settings;
	}

	ngOnInit(): void
	{
		this.activatedRoute.params.forEach
		(
			(params: Params) =>
			{
				if (params['id'] !== undefined) { this.employeeId = params['id']; }
			}
		);

		this.getData();
	}

	routeToCreate()
	{
		this.router.navigate(['/dependent/create']);
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
		this.dependentService
			.getByEmployeeId(this.employeeId)
			.subscribe
			(
				results =>
				{
					this.dependents = results;

					this.dependents.forEach
					(
						dependent =>
						{
						}
					);

					this.temp = [...this.dependents];
					this.rows = this.dependents;
					setTimeout(() => { this.loadingIndicator = false; }, 1500);
				},
				error => console.error(error)
			);
	}

	delete(dependentId: string)
	{
		this.dependentService
			.delete(dependentId)
			.subscribe
			(
				result => this.getData(),
				error => console.error(error)
			);
	}

	goToDetail(id: string) { this.router.navigate(['/dependent/edit/' + id]); }
}
