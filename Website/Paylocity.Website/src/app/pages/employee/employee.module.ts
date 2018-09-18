import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '../../shared/shared.module';

import { EmployeeComponent } from './employee.component';
import { EmployeeCreateComponent } from './create/create.component';
import { EmployeeEditComponent } from './edit/edit.component';
import { EmployeeService } from './employee.service';

export const routes =
[
	{ path: '', component: EmployeeComponent, pathMatch: 'full', data: { breadcrumb: 'Employee' } },
	{ path: 'create', component: EmployeeCreateComponent, pathMatch: 'full', data: { breadcrumb: 'Employee Create' } },
	{ path: 'edit/:id', component: EmployeeEditComponent, pathMatch: 'full', data: { breadcrumb: 'Employee Edit' } }
];

@NgModule
(
	{
		imports:
		[
			CommonModule,
			RouterModule.forChild(routes),
			NgxDatatableModule,
			SharedModule,
			FormsModule,
			ReactiveFormsModule
		],
		declarations:
		[
			EmployeeComponent,
			EmployeeCreateComponent,
			EmployeeEditComponent
		],
		providers:
		[
			EmployeeService
		]
	}
)

export class EmployeeModule { }
