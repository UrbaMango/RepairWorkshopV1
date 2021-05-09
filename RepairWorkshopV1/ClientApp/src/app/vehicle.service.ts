import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Client } from '../Models/Client';
import { User } from '../Models/User';
import { ClientCreate } from '../Models/ClientCreate'
import { ClientEdit } from '../models/ClientEdit';
import { Vehicle } from '../models/Vehicle';
import { VehicleCreate } from '../Models/VehicleCreate';
import { VehicleEdit } from '../Models/VehicleEdit';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  baseUrl = environment.baseUrl;
  clientsUrl = 'api/Clients';
  clientUserCreateUrl = 'api/UsersClients';
  vehiclesUrl = 'api/Vehicles';
  multipleVehiclesUrl = 'api/Vehicles/multiple'

  constructor(private http: HttpClient) { }

  GetVehicles(): Observable<any> {
    return this.http.get<[Vehicle[]]>(`${this.baseUrl}/${this.vehiclesUrl}`);
  }

  GetClientVehicles(): Observable<any> {
    return this.http.get<[Vehicle[]]>(`${this.baseUrl}/${this.vehiclesUrl}/${localStorage.getItem('clientId')}`)
  }

  AddNewVehicle(vehicle: VehicleCreate): Observable<any> {
    return this.http.post<VehicleCreate>(`${this.baseUrl}/${this.vehiclesUrl}`, vehicle);
  }

  AddMultipleVehicles(vehicles: VehicleCreate[]): Observable<any> {
    return this.http.post<VehicleCreate[]>(`${this.baseUrl}/${this.multipleVehiclesUrl}`, vehicles);
  }

  DeleteVehicle(vehicleId): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/${this.vehiclesUrl}/${vehicleId}`);
  }

  EditVehicle(vehicle: VehicleEdit): Observable<any> {
    return this.http.put<VehicleEdit>(`${this.baseUrl}/${this.vehiclesUrl}/${vehicle.vehicleId}`, vehicle);
  }

}
