import { Component, OnInit } from '@angular/core';
import { LoginClientComponent } from '../login-client/login-client.component';
import { LoginEmployeeComponent } from '../login-employee/login-employee.component';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
