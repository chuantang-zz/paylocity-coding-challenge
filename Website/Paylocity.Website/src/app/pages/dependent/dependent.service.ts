import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

// models
import { Dependent } from './dependent.model';

// helper
import { environment } from '../../../environments/environment';
import { LocalStorageHelper } from '../../utility/helper/localstorage.helper';

@Injectable()
export class DependentService
{
	constructor(private http: HttpClient, private localStorageHelper: LocalStorageHelper) { }

	private baseUrl = environment.baseUrl + 'api/Dependent';

	public get(): Observable<Dependent[]>
	{
		const httpHeaders = this.createHeader();

		console.log(this.baseUrl);
		return this.http.get<Dependent[]>(this.baseUrl, { headers: httpHeaders });
	}

	public getByDependentId(id: string): Observable<Dependent>
	{
		const requestUrl = this.baseUrl + '/' + id;
		const httpHeaders = this.createHeader();

		return this.http.get<Dependent>(requestUrl, { headers: httpHeaders });
	}

	public getByEmployeeId(id: string): Observable<Dependent[]>
	{
		const requestUrl = this.baseUrl + '/EmployeeId/' + id;
		const httpHeaders = this.createHeader();

		return this.http.get<Dependent[]>(requestUrl, { headers: httpHeaders });
	}

	public update(dependent: Dependent): Observable<Dependent>
	{
		const requestUrl = this.baseUrl + '/' + dependent.dependentId;
		const httpHeaders = this.createHeader();

		return this.http.put<Dependent>(requestUrl, JSON.stringify(dependent), { headers: httpHeaders });
	}

	public create(dependent: Dependent): Observable<Dependent>
	{
		const httpHeaders = this.createHeader();

		return this.http.post<Dependent>(this.baseUrl, JSON.stringify(dependent), { headers: httpHeaders });
	}

	public delete(dependentId: string): Observable<Dependent>
	{
		const requestUrl = this.baseUrl + '?id=' + dependentId;
		const httpHeaders = this.createHeader();

		return this.http.delete<Dependent>(requestUrl, { headers: httpHeaders });
	}

	private createHeader(): HttpHeaders
	{
		const httpHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

		return httpHeaders;
	}
}
