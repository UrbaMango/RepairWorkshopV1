import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Client } from '../Models/Client';
import { User } from '../Models/User';
import { ClientCreate } from '../Models/ClientCreate'
import { ClientEdit } from '../models/ClientEdit';


@Injectable({
  providedIn: 'root'
})
export class ClientService {

  baseUrl = environment.baseUrl;
  clientsUrl = 'api/Clients';
  clientUserCreateUrl = 'api/UsersClients';

  constructor(private http: HttpClient) { }

  LoginEmployee(): Observable<any> {
    return this.http.get<Client[]>(`${this.baseUrl}/${this.clientsUrl}`);
  }

  GetClientId(): Observable<any> {
    return this.http.get<Client[]>(`${this.baseUrl}/${this.clientsUrl}/${localStorage.getItem('username')}`).pipe(tap(resp => {
      localStorage.setItem('clientId', resp[0].clientId);
      }));
    }

  CreateNewClientUser(user: User, client: ClientCreate): Observable<any> {

    var reqHeader = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });

    return this.http.post<User>(`${this.baseUrl}/${this.clientUserCreateUrl}`, user, { headers: reqHeader }).pipe(tap(resp => {
      client.userId = resp.userId;
      this.CreateNewClient(client).subscribe(
       (): void => {
          console.log("Client service: USER CREATED")
        }
      );;
    }));
  }

  CreateNewClient(newClient: ClientCreate): Observable<any> {
    //console.log("lop " + client + client.UserId + " " + client.Email + " " + client.ClientName + " " + client.Address + " " + client.PhoneNumber + " " + client.RegistrationCode);
    return this.http.post<ClientCreate>(`${this.baseUrl}/${this.clientsUrl}`, newClient);
  }

  DeleteClient(clientId): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/${this.clientsUrl}/${clientId}`).pipe(tap(resp => {

      var reqHeader = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
      });
      console.log(resp.userId);
      this.http.delete<any>(`${this.baseUrl}/${this.clientUserCreateUrl}/${resp.userId}`, { headers: reqHeader }).subscribe((): void => {
        console.log("Client user deleted in user table");
      })

    }));; 
  }

  EditClient(clientEdit: ClientEdit): Observable<any> {
    console.log("isijungia");
    return this.http.put<any>(`${this.baseUrl}/${this.clientsUrl}/${clientEdit.clientId}`, clientEdit);
  }




}
