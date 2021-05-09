import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehiclesTabComponent } from './vehicles-tab.component';

describe('VehiclesTabComponent', () => {
  let component: VehiclesTabComponent;
  let fixture: ComponentFixture<VehiclesTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VehiclesTabComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VehiclesTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
