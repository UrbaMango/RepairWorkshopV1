import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { LoginClientComponent } from './login-client/login-client.component';
import { LoginEmployeeComponent } from './login-employee/login-employee.component';
import { AppRoutingModule } from './app-routing.module';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { EmployeeNavComponent } from './employee-nav/employee-nav.component';
import { ClientNavComponent } from './client-nav/client-nav.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { ClientTabComponent } from './client-tab/client-tab.component';
import { VehiclesTabComponent } from './vehicles-tab/vehicles-tab.component';
import { VisitsTabComponent } from './visits-tab/visits-tab.component';
import { NotImplementedComponent } from './not-implemented/not-implemented.component';
import { HomeTabComponent } from './home-tab/home-tab.component';
import { ClientVehicleTabComponent } from './client-vehicle-tab/client-vehicle-tab.component';
import { ClientVisitTabComponent } from './client-visit-tab/client-visit-tab.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginClientComponent,
    LoginEmployeeComponent,
    LoginComponent,
    EmployeeNavComponent,
    ClientNavComponent,
    ClientTabComponent,
    VehiclesTabComponent,
    VisitsTabComponent,
    NotImplementedComponent,
    HomeTabComponent,
    ClientVehicleTabComponent,
    ClientVisitTabComponent
  ],
  imports: [
    MDBBootstrapModule.forRoot(),
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
