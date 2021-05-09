import { Component, OnInit } from '@angular/core';
import { AuthEmployeeService } from '../auth-employee.service';
import { Router, RouterModule, Routes } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { catchError, map, retry } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import 'rxjs/add/operator/map';
import { ClientTabComponent } from '../client-tab/client-tab.component';

@Component({
  selector: 'app-employee-nav',
  templateUrl: './employee-nav.component.html',
  styleUrls: ['./employee-nav.component.css']
})
export class EmployeeNavComponent implements OnInit {

  AuthorizeUrl = 'https://localhost:44310/employee/authorized';

  

  constructor(private authService: AuthEmployeeService, private router: Router, private http: HttpClient) {

    var token = 'Bearer ' + localStorage.getItem('token');

    const headers = new HttpHeaders()
      .set('Authorization', token);

    this.http.get(this.AuthorizeUrl, { 'headers': headers });
    
  }

  logOutClick(): void {
    this.router.navigateByUrl('employee-login');
  }

  ngOnInit(): void {}

}
