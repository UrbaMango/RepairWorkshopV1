import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Client } from '../Models/Client';
import { User } from '../Models/User';
import { ClientCreate } from '../Models/ClientCreate'
import { ClientEdit } from '../models/ClientEdit';
import { Visit } from '../Models/Visit';
import { VisitCreate } from '../Models/VisitCreate';
import { VisitEdit} from '../Models/VisitEdit';


@Injectable({
  providedIn: 'root'
})
export class VisitService {

  baseUrl = environment.baseUrl;
  clientsUrl = 'api/Clients';
  clientUserCreateUrl = 'api/UsersClients';
  vehiclesUrl = 'api/Vehicles';
  visitsUrl = 'api/Visits';

  constructor(private http: HttpClient) { }

  GetVisits(): Observable<any> {
    return this.http.get<[Visit[]]>(`${this.baseUrl}/${this.visitsUrl}`);
  }

  GetClientVisits(): Observable<any> {
    return this.http.get<Visit[]>(`${this.baseUrl}/${this.visitsUrl}/${localStorage.getItem('clientId')}`);
  }

  EditVisit(visit: VisitEdit): Observable<any> {
    return this.http.put<VisitEdit>(`${this.baseUrl}/${this.visitsUrl}/${visit.visitId}`, visit);
  }

  CreateVisit(visit: VisitCreate): Observable<any> {
    return this.http.post<VisitCreate>(`${this.baseUrl}/${this.visitsUrl}`, visit);
  }

  DeleteVisit(visitId): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/${this.visitsUrl}/${visitId}`);
  }

}
