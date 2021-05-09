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

@Component({
  selector: 'app-client-tab',
  templateUrl: './client-tab.component.html',
  styleUrls: ['./client-tab.component.css']
})
export class ClientTabComponent implements OnInit {

  constructor(private clientService: ClientService, private router: Router) { }

  ngOnInit(): void {
    this.clientService.LoginEmployee().subscribe(clients => this.clients = clients);
  }

  clients: Client;
  user: User;
  clientCreate: ClientCreate;
  clientEdit: ClientEdit;

  fileName = 'ClientExport.xlsx';

  showTable: boolean = true;
  showNewUser: boolean = false;
  showEditUser: boolean = false;

  private sorted = false;

  editFormClientName = "";
  editFormAddress = "";
  editFormEmail = "";
  editFormPhone: number;
  editFormRegistrationCode: number;
  editFormCompanyAccount = false;
  clientId: number;
  userId: number;

  username = "";
  password = "";
  clientName = "";
  address = "";
  phoneNumber: number;
  registrationCode: number;

  createNewUser(): void {
    if (this.showNewUser)
      this.showNewUser = false;
    else
      this.showNewUser = true;
  }

  createNewClientButton(): void {

    this.user = {
      username: this.username,
      password: this.password,
    };

    this.clientCreate = {
      userId: 10,
      email: this.username,
      clientName: this.clientName,
      address: this.address,
      phoneNumber: this.phoneNumber,
      registrationCode: this.registrationCode,
      companyAccount: true
    };

    this.clientService.CreateNewClientUser(this.user, this.clientCreate).subscribe(
      (): void => {
        console.log("created new user");
        this.router.navigateByUrl('employee-nav/client-tab');
      }
   );;
    console.log("Create new user button pressed");

    //this.refresh();

  }

  refresh(): void {
    window.location.reload();
  }

  editUser(EclientId, EuserId, Eemail, EclientName, Eaddress, EphoneNumber, Eregistrationcode): void {
    //client.clientId, client.userId, client.email, client.companyAccount, client.clientName, client.address, client.phoneNumber, client.registrationCode
    this.showNewUser = false;

    if (this.showEditUser)
      this.showEditUser = false;
    else
      this.showEditUser = true;

    this.clientId = EclientId;
    this.userId = EuserId;
    this.editFormClientName = EclientName;
    this.editFormAddress = Eaddress;
    this.editFormEmail = Eemail;
    this.editFormPhone = EphoneNumber;
    this.editFormRegistrationCode = Eregistrationcode;

  }

  editUserButton(): void {

    this.clientEdit = {
      clientId: this.clientId,
      userId: this.userId,
      email: this.editFormEmail,
      clientName: this.editFormClientName,
      address: this.editFormAddress,
      phoneNumber: this.editFormPhone,
      registrationCode: this.editFormRegistrationCode
    };

    if (confirm("Submit changes?")) {
      this.clientService.EditClient(this.clientEdit).subscribe(() => {
        console.log(this.clientEdit.email + " has been updated");
      });
    }
    //this.refresh();
  }
  
  deleteUser(clientId, email): void {
    if (confirm("Are you sure you want to delete " + email)) {
      console.log("Implement delete functionality here");
      this.clientService.DeleteClient(clientId).subscribe((): void => {
        console.log("Client has been deleted");
      })

      //this.refresh();
    }


  }

  exportexcel(): void {
    /* table id is passed over here */
    let element = document.getElementById('excel-table');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);

  }

  sortBy(by: string | any): void {

    /*
     this.clients.sort((a: any, b: any) => {
      if (a[by] < b[by]) {
        return this.sorted ? 1 : -1;
      }
      if (a[by] > b[by]) {
        return this.sorted ? -1 : 1;
      }

      return 0;
    });

    this.sorted = !this.sorted;
    */
  }

}
