import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

import { PagesComponent } from './pages/pages.component';
import { NotFoundComponent } from './pages/errors/not-found/not-found.component';
import { ErrorComponent } from './pages/errors/error/error.component';

export const routes: Routes =
[
	{
		path: '',
		component: PagesComponent, children:
		[
			{ path: '', redirectTo: '/dashboard', pathMatch: 'full' },
			{ path: 'dashboard', loadChildren: '../app/pages/dashboard/dashboard.module#DashboardModule', data: { breadcrumb: 'Dashboard' } },
			{ path: 'employee', loadChildren: '../app/pages/employee/employee.module#EmployeeModule', data: { breadcrumb: 'Employee' } },
			{ path: 'dependent', loadChildren: '../app/pages/dependent/dependent.module#DependentModule', data: { breadcrumb: 'Dependent' } }
		]
	},
	{ path: 'error', component: ErrorComponent, data: { breadcrumb: 'Error' } },
	{ path: '**', component: NotFoundComponent }
];

export const routing: ModuleWithProviders = RouterModule.forRoot
(
	routes,
	{
		preloadingStrategy: PreloadAllModules,  // <- comment this line for activate lazy load
		// useHash: true
	}
);
