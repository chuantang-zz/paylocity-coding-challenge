import { ServiceWorkerModule } from '@angular/service-worker';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { OverlayContainer } from '@angular/cdk/overlay';
import { CustomOverlayContainer } from './theme/utils/custom-overlay-container';

// helper
import { AuthGuard } from './utility/helper/authGuard.helper';
import { LocalStorageHelper } from './utility/helper/localstorage.helper';
import { DateHelper } from './utility/helper/date.helper';

import { AgmCoreModule } from '@agm/core';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

import { CalendarModule } from 'angular-calendar';
import { SharedModule } from './shared/shared.module';
import { PipesModule } from './theme/pipes/pipes.module';
import { routing } from './app.routing';

import { AppComponent } from './app.component';
import { PagesComponent } from './pages/pages.component';
import { NotFoundComponent } from './pages/errors/not-found/not-found.component';
import { ErrorComponent } from './pages/errors/error/error.component';
import { AppSettings } from './app.settings';

import { SidenavComponent } from './theme/components/sidenav/sidenav.component';
import { VerticalMenuComponent } from './theme/components/menu/vertical-menu/vertical-menu.component';
import { BreadcrumbComponent } from './theme/components/breadcrumb/breadcrumb.component';
import { FlagsMenuComponent } from './theme/components/flags-menu/flags-menu.component';
import { FullScreenComponent } from './theme/components/fullscreen/fullscreen.component';
import { ApplicationsComponent } from './theme/components/applications/applications.component';
import { MessagesComponent } from './theme/components/messages/messages.component';
import { UserMenuComponent } from './theme/components/user-menu/user-menu.component';
import { environment } from '../environments/environment';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface =
{
	wheelPropagation: true,
	suppressScrollX: true
};

@NgModule
(
	{
		declarations:
		[
			AppComponent, PagesComponent, NotFoundComponent, ErrorComponent, SidenavComponent, VerticalMenuComponent, BreadcrumbComponent, FlagsMenuComponent, FullScreenComponent,
			ApplicationsComponent, MessagesComponent, UserMenuComponent
		],
		entryComponents: [ VerticalMenuComponent ],
		imports:
		[
			BrowserModule, BrowserAnimationsModule, FormsModule, ReactiveFormsModule, HttpClientModule, PerfectScrollbarModule, CalendarModule.forRoot(), SharedModule, PipesModule, routing,
			AgmCoreModule.forRoot({ apiKey: 'AIzaSyDe_oVpi9eRSN99G4o6TwVjJbFBNr58NxE' }), // Google Maps
			ServiceWorkerModule.register('/ngsw-worker.js', { enabled: environment.production })
		],
		providers:
		[
			AppSettings, { provide: PERFECT_SCROLLBAR_CONFIG, useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG }, { provide: OverlayContainer, useClass: CustomOverlayContainer },

			// helper providers
			AuthGuard, DateHelper, LocalStorageHelper
		],
		bootstrap: [ AppComponent ]
	}
)

export class AppModule { }
