import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Observable } from 'rxjs';
import { error } from 'protractor';
import { Token } from '@angular/compiler';
import { User } from '../../Models/User';
import { ClientService } from '../client.service';
import { Router, RouterModule, Routes } from '@angular/router';
import { Client } from '../../Models/Client';
import { ClientCreate } from '../../models/ClientCreate';
import { ClientEdit } from '../../models/ClientEdit';
import * as XLSX from 'xlsx';
import { Vehicle } from '../../Models/Vehicle';
import { VehicleCreate } from '../../Models/VehicleCreate';
import { VehicleEdit } from '../../Models/VehicleEdit';
import { VehicleService } from '../vehicle.service';
import { VisitService } from '../visit.service';
import { Visit } from '../../Models/Visit';
import { VisitEdit } from '../../Models/VisitEdit';
import { VisitCreate } from '../../Models/VisitCreate';

@Component({
  selector: 'app-client-visit-tab',
  templateUrl: './client-visit-tab.component.html',
  styleUrls: ['./client-visit-tab.component.css']
})
export class ClientVisitTabComponent implements OnInit {

  constructor(private clientService: ClientService, private visitService: VisitService, private vehicleService: VehicleService, private router: Router,) { }


  visits: Visit;
  visitEdit: VisitEdit;
  visitCreate: VisitCreate;

  ngOnInit(): void {
    this.visitService.GetClientVisits().subscribe(visits => this.visits = visits);
  }

  visitId: number;
  visitClientId: number;
  visitVehicleId: number;
  visitMileage: number;
  visitStartDate: Date;
  visitEndDate: Date;
  visitNotes = "";
  visitPrice: number;
  visitConfirmed: boolean;
  visitProgress = ""

  fileName = 'visitExport.xlsx';

  showTable: boolean = true;

  showNewVisitInput: boolean = false;
  showEditVisitInput: boolean = false;

  showEditVisit(visitId, clientId, vehicleId, mileage, visitStartDate, visitEndDate, notes, visitPrice, confirmed, progress): void {
    if (this.showEditVisitInput)
      this.showEditVisitInput = false;
    else
      this.showEditVisitInput = true;

    this.visitId = visitId;
    this.visitClientId = clientId;
    this.visitVehicleId = vehicleId;
    this.visitMileage = mileage;
    this.visitStartDate = visitStartDate;
    this.visitEndDate = visitEndDate;
    this.visitNotes = notes;
    this.visitPrice = visitPrice;
    this.visitConfirmed = confirmed;
    this.visitProgress = progress;

  }

  editVisitButton(): void {

    this.visitEdit = {
      visitId: this.visitId,
      clientId: this.visitClientId,
      vehicleId: this.visitVehicleId,
      mileage: this.visitMileage,
      visitStartDate: this.visitStartDate,
      visitEndDate: this.visitEndDate,
      notes: this.visitNotes,
      visitPrice: this.visitPrice,
      confirmed: this.visitConfirmed,
      progress: this.visitProgress
    }

    this.visitService.EditVisit(this.visitEdit).subscribe(
      (): void => {
        console.log("Visit has been updated");
      }
    );;

    this.refresh();
  }

  showNewVehicle(): void {
    if (this.showNewVisitInput)
      this.showNewVisitInput = false;
    else
      this.showNewVisitInput = true;
  }

  deleteVisit(visitId): void {
    if (confirm("Are you sure you want to delete this visit " + visitId)) {
      console.log("Implement delete functionality here");
      this.visitService.DeleteVisit(visitId).subscribe((): void => {
        console.log("Visit has been deleted");
      })

      this.refresh();
    }
  }

  exportexcel(): void {
    /* table id is passed over here */
    let element = document.getElementById('visit-data');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);

  }

  refresh(): void {
    window.location.reload();
  }

}
