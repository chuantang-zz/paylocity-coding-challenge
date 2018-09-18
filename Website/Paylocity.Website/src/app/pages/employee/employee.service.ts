import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

// models
import { Employee } from './employee.model';

// helper
import { environment } from '../../../environments/environment';
import { LocalStorageHelper } from '../../utility/helper/localstorage.helper';

@Injectable()
export class EmployeeService
{
	constructor(private http: HttpClient, private localStorageHelper: LocalStorageHelper) { }

	private baseUrl = environment.baseUrl + 'api/Employee';

	public get(): Observable<Employee[]>
	{
		const httpHeaders = this.createHeader();

		console.log(this.baseUrl);
		return this.http.get<Employee[]>(this.baseUrl, { headers: httpHeaders });
	}

	public getByEmployeeId(id: string): Observable<Employee>
	{
		const requestUrl = this.baseUrl + '/' + id;
		const httpHeaders = this.createHeader();

		return this.http.get<Employee>(requestUrl, { headers: httpHeaders });
	}

	public update(employee: Employee): Observable<Employee>
	{
		const requestUrl = this.baseUrl + '/' + employee.employeeId;
		const httpHeaders = this.createHeader();

		return this.http.put<Employee>(requestUrl, JSON.stringify(employee), { headers: httpHeaders });
	}

	public create(employee: Employee): Observable<Employee>
	{
		const httpHeaders = this.createHeader();

		return this.http.post<Employee>(this.baseUrl, JSON.stringify(employee), { headers: httpHeaders });
	}

	public delete(employeeId: string): Observable<Employee>
	{
		const requestUrl = this.baseUrl + '?id=' + employeeId;
		const httpHeaders = this.createHeader();

		return this.http.delete<Employee>(requestUrl, { headers: httpHeaders });
	}

	private createHeader(): HttpHeaders
	{
		const httpHeaders: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

		return httpHeaders;
	}
}
