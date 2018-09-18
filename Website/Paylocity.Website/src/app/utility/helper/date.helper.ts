import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class DateHelper
{
	constructor(private route: Router) { }

	public getParsedDate(inputedDate: string): Date
	{
		const parsedDate = new Date(inputedDate);

		return parsedDate;
	}

	public getTodaysDate(): Date
	{
		const todaysDateTime = new Date();
		const todaysDate = todaysDateTime.getUTCFullYear() + '-' + (todaysDateTime.getUTCMonth() + 1) + '-' + todaysDateTime.getUTCDate();
		const parsedTodaysDate = this.getParsedDate(todaysDate);

		return parsedTodaysDate;
	}
}
