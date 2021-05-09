import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientVehicleTabComponent } from './client-vehicle-tab.component';

describe('ClientVehicleTabComponent', () => {
  let component: ClientVehicleTabComponent;
  let fixture: ComponentFixture<ClientVehicleTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientVehicleTabComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientVehicleTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
