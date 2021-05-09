import { Component, OnInit } from '@angular/core';
import { ClientService } from '../client.service';

@Component({
  selector: 'app-client-nav',
  templateUrl: './client-nav.component.html',
  styleUrls: ['./client-nav.component.css']
})
export class ClientNavComponent implements OnInit {

  constructor(private clientService: ClientService) { }

  ngOnInit(): void {
    this.clientService.GetClientId().subscribe();
  }

}
