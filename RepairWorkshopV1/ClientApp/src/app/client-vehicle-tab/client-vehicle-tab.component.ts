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
import { VisitCreate } from '../../Models/VisitCreate';

@Component({
  selector: 'app-client-vehicle-tab',
  templateUrl: './client-vehicle-tab.component.html',
  styleUrls: ['./client-vehicle-tab.component.css']
})
export class ClientVehicleTabComponent implements OnInit {

  constructor(private clientService: ClientService, private visitService: VisitService, private vehicleService: VehicleService, private router: Router,) { }

  vehicles: Vehicle;
  vehicleCreate: VehicleCreate;
  vehicleEdit: VehicleEdit;

  importVehicles: Vehicle[];

  visit: VisitCreate;

  ngOnInit(): void {
    this.vehicleService.GetClientVehicles().subscribe(vehicles => this.vehicles = vehicles);
  }

  vehicleMake = "";
  vehicleModel = "";
  vehicleVin = "";
  vehicleYear = "";
  vehicleLicensePlate = "";
  vehicleClientId: number;
  vehicleId: number;

  clientId: number;

  visitMileage: number;
  visitStartDate: Date;
  visitNotes = "";

  fileName = 'VehicleExport.xlsx';

  showTable: boolean = true;

  showNewVehicleInput: boolean = false;
  showEditVehicleInput: boolean = false;
  showNewVisitInput: boolean = false;

  showEditVehicle(vehicleId, make, model, vin, year, licensePlate): void {
    if (this.showEditVehicleInput)
      this.showEditVehicleInput = false;
    else
      this.showEditVehicleInput = true;

    this.vehicleMake = make;
    this.vehicleModel = model;
    this.vehicleVin = vin;
    this.vehicleYear = year;
    this.vehicleLicensePlate = licensePlate;
    this.vehicleClientId = Number(localStorage.getItem('clientId'));
    this.vehicleId = vehicleId;

  }

  editVehicleButton(): void {

    this.vehicleEdit = {
      make: this.vehicleMake,
      vin: this.vehicleVin,
      model: this.vehicleModel,
      year: this.vehicleYear,
      licensePlate: this.vehicleLicensePlate,
      clientId: this.vehicleClientId,
      vehicleId: this.vehicleId
    }

    this.vehicleService.EditVehicle(this.vehicleEdit).subscribe(
      (): void => {
        console.log("Vehicle had been updated");
      }
    );;

    this.refresh();
  }

  showNewVehicle(): void {
    if (this.showNewVehicleInput)
      this.showNewVehicleInput = false;
    else
      this.showNewVehicleInput = true;
  }

  createNewVehicleButton(): void {

    this.vehicleCreate = {
      make: this.vehicleMake,
      vin: this.vehicleVin,
      model: this.vehicleModel,
      year: this.vehicleYear,
      licensePlate: this.vehicleLicensePlate,
      clientId: Number(localStorage.getItem('clientId'))
    };

    this.vehicleService.AddNewVehicle(this.vehicleCreate).subscribe(
      (): void => {
        console.log("new Vehicle Added");
      }
    );;

    //this.refresh();

  }

  deleteVehicle(vehicleId): void {

    if (confirm("Are you sure you want to delete " + vehicleId)) {
      console.log("Implement delete functionality here");
      this.vehicleService.DeleteVehicle(vehicleId).subscribe((): void => {
        console.log("Vehicle has been deleted");
      })

      this.refresh();
    }
  }

  showNewVisit(vehicleId, clientId): void {
    if (this.showNewVisitInput)
      this.showNewVisitInput = false;
    else
      this.showNewVisitInput = true;

    this.vehicleId = vehicleId;
    this.clientId = clientId;
  }

  createNewVisitButton(): void {
    this.visit = {
      clientId: Number(localStorage.getItem('clientId')),
      vehicleId: this.vehicleId,
      mileage: this.visitMileage,
      visitStartDate: this.visitStartDate,
      notes: this.visitNotes,
      confirmed: true,
      progress: "In progress"
    }

    this.visitService.CreateVisit(this.visit).subscribe(
      (): void => {
        console.log("new visit Added");
        this.refresh();
      }
    );;;

  }

  async refresh() {
    await window.location.reload();
  }

  exportexcel(): void {
    /* table id is passed over here */
    let element = document.getElementById('vehicle-data');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);

  }

  public importFromFile(bstr: string): XLSX.AOA2SheetOpts {
    /* read workbook */
    const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });

    /* grab first sheet */
    const wsname: string = wb.SheetNames[0];
    const ws: XLSX.WorkSheet = wb.Sheets[wsname];

    /* save data */
    const data = <XLSX.AOA2SheetOpts>(XLSX.utils.sheet_to_json(ws, { header: 1 }));

    return data;
  }

}
