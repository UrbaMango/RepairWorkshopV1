import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { User } from '../Models/User';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Config } from 'protractor/built/config';


@Injectable({
  providedIn: 'root'
})
export class AuthEmployeeService {

  baseUrl = environment.baseUrl;
  LoginEmpUrl = 'employee/login';
  LoginClientUrl = 'client/login';
  AuthorizeUrl = 'employee/authorized';

  constructor(private http: HttpClient) { }

  LoginEmployee(user: User): Observable<any> {
    return this.http.post<User>(`${this.baseUrl}/${this.LoginEmpUrl}`, user).pipe(tap(resp => {
      localStorage.setItem('userId', resp.userId);
      localStorage.setItem('token', resp.token);
      localStorage.setItem('username', resp.username);
    }))
  }

  LoginClient(user: User): Observable<any> {
    return this.http.post<User>(`${this.baseUrl}/${this.LoginClientUrl}`, user).pipe(tap(resp => {
      localStorage.setItem('token', resp.token);
      localStorage.setItem('userId', resp.userId);
      localStorage.setItem('username', resp.username);
    }))
  }

  AuthorizedEmployeeCheck(): Observable<any> {
    return this.http.get<Response>(`${this.baseUrl} / ${this.AuthorizeUrl}`, { observe: 'response' });
  }

}
