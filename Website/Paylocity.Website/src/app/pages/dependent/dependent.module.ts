import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '../../shared/shared.module';

import { DependentComponent } from './dependent.component';
import { DependentCreateComponent } from './create/create.component';
import { DependentEditComponent } from './edit/edit.component';
import { DependentService } from './dependent.service';

export const routes =
[
	{ path: '', component: DependentComponent, pathMatch: 'full', data: { breadcrumb: 'Dependent' } },
	{ path: 'create/:id', component: DependentCreateComponent, pathMatch: 'full', data: { breadcrumb: 'Dependent Create' } },
	{ path: 'edit/:id', component: DependentEditComponent, pathMatch: 'full', data: { breadcrumb: 'Dependent Edit' } }
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
			DependentComponent,
			DependentCreateComponent,
			DependentEditComponent
		],
		providers:
		[
			DependentService
		]
	}
)

export class DependentModule { }
