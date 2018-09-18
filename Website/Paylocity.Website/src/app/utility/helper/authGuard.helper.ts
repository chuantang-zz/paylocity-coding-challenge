import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';

import { LocalStorageHelper } from './localstorage.helper';

@Injectable()
export class AuthGuard implements CanActivate
{
	constructor(private router: Router, private localStorageHelper: LocalStorageHelper) { }

	canActivate(): boolean
	{
		let isAuthorized: boolean = this.isLoggedIn();

		if (!isAuthorized)
		{
			this.router.navigate(['/login']);
			isAuthorized = false;
		}

		return isAuthorized;
	}

	public isLoggedIn(): boolean
	{
		const hasAuthCode = this.localStorageHelper.authTokenGet();

		if (hasAuthCode === '') return false;

		return true;
	}
}
