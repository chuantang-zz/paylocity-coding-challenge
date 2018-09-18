import { Injectable } from '@angular/core';

// models
import { AuthToken } from '../authToken.model';

@Injectable()
export class LocalStorageHelper
{
	constructor() { }

	public authTokenGet(): string
	{
		const localStorageAuthToken = localStorage.getItem('auth_token');
		let authToken = '';

		if (localStorageAuthToken === undefined || localStorageAuthToken === null) authToken = '';
		else authToken = localStorageAuthToken;

		return authToken;
	}

	public authTokenSet(authToken: AuthToken)
	{
		localStorage.setItem('auth_token', authToken.auth_token);
		localStorage.setItem('userId', authToken.id);
		localStorage.setItem('expires_in', authToken.expires_in);
	}

	public userId(): string
	{
		const localStorageUserId = localStorage.getItem('userId');
		let userId = '';

		if (localStorageUserId === undefined || localStorageUserId === null) userId = '';
		else userId = localStorageUserId;

		return userId;
	}

	public propertyIdGet(): string
	{
		const localStoragePropertyId = localStorage.getItem('propertyId');
		let propertyId = '';

		if (localStoragePropertyId === undefined || localStoragePropertyId === null) propertyId = '';
		else propertyId = localStoragePropertyId;

		return propertyId;
	}

	public propertyIdSet(propertyId: string) { localStorage.setItem('propertyId', propertyId); }
}
