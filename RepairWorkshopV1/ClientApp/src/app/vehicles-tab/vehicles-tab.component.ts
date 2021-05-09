import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Observable } from 'rxjs';
import { error } from 'protractor';
import { Token } from '@angular/compiler';
import { User } from '../../Models/User';
import { Router, RouterModule, Routes } from '@angular/router';
import { Client } from '../../Models/Client';
import { ClientCreate } from '../../models/ClientCreate';
import { ClientEdit } from '../../models/ClientEdit';
import * as XLSX from 'xlsx';
import { Vehicle } from '../../Models/Vehicle';
import { VehicleCreate } from '../../Models/VehicleCreate';
import { VehicleEdit} from '../../Models/VehicleEdit';
import { VisitCreate } from '../../Models/VisitCreate';
import { VehicleService } from '../vehicle.service';
import { ClientService } from '../client.service';
import { VisitService } from '../visit.service'


@Component({
  selector: 'app-vehicles-tab',
  templateUrl: './vehicles-tab.component.html',
  styleUrls: ['./vehicles-tab.component.css']
})
export class VehiclesTabComponent implements OnInit {

  constructor(private clientService: ClientService, private visitService: VisitService, private vehicleService: VehicleService, private router: Router,) { }

  vehicles: Vehicle;
  vehicleCreate: VehicleCreate;
  vehicleEdit: VehicleEdit;

  visit: VisitCreate;

  ngOnInit(): void {
    this.vehicleService.GetVehicles().subscribe(vehicles => this.vehicles = vehicles);
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

  showTable: boolean = true;

  showNewVehicleInput: boolean = false;
  showEditVehicleInput: boolean = false;
  showNewVisitInput: boolean = false;

  showEditVehicle(vehicleId, make, model, vin, year, licensePlate, clientId): void {
    this.showNewVehicleInput = false;
    this.showNewVisitInput = false;

    if (this.showEditVehicleInput)
      this.showEditVehicleInput = false;
    else
      this.showEditVehicleInput = true;

    this.vehicleMake = make;
    this.vehicleModel = model;
    this.vehicleVin = vin;
    this.vehicleYear = year;
    this.vehicleLicensePlate = licensePlate;
    this.vehicleClientId = clientId;
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
    this.showEditVehicleInput = false;
    this.showNewVisitInput = false;

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
      clientId: this.vehicleClientId
    };

    this.vehicleService.AddNewVehicle(this.vehicleCreate).subscribe(
      (): void => {
        console.log("new Vehicle Added");
      }
    );;

    this.refresh();

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
      clientId: this.clientId,
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

  async refresh(){
    await window.location.reload();
  }

}
