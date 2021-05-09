import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginClientComponent } from './login-client/login-client.component';
import { LoginEmployeeComponent } from './login-employee/login-employee.component';
import { LoginComponent } from './login/login.component';
import { EmployeeNavComponent } from './employee-nav/employee-nav.component';
import { ClientNavComponent } from './client-nav/client-nav.component';
import { ClientTabComponent } from './client-tab/client-tab.component';
import { NotImplementedComponent } from './not-implemented/not-implemented.component';
import { VehiclesTabComponent } from './vehicles-tab/vehicles-tab.component';
import { VisitsTabComponent } from './visits-tab/visits-tab.component';
import { HomeTabComponent } from './home-tab/home-tab.component';
import { ClientVehicleTabComponent } from './client-vehicle-tab/client-vehicle-tab.component';
import { ClientVisitTabComponent } from './client-visit-tab/client-visit-tab.component';

const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'client-login', component: LoginClientComponent },
    { path: 'employee-login', component: LoginEmployeeComponent },
    { path: 'employee-nav', component: EmployeeNavComponent,
    children:
      [
        { path: 'home', component: HomeTabComponent },
        { path: 'client-tab', component: ClientTabComponent },
        { path: 'vehicle-tab', component: VehiclesTabComponent },
        { path: 'visit-tab', component: VisitsTabComponent }
      ] 
    },
     {
    path: 'client-nav', component: ClientNavComponent,
    children:
      [
        { path: 'home', component: HomeTabComponent },
        { path: 'vehicles', component: ClientVehicleTabComponent },
        { path: 'visits', component: ClientVisitTabComponent }
      ]
     },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'not-implemented', component: NotImplementedComponent },
  { path: '*', redirectTo: '/login', pathMatch: 'full' }
];


@NgModule({
  declarations: [],
    imports: [
      RouterModule.forRoot(routes),
      CommonModule
  ]
})
export class AppRoutingModule { }
