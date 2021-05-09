import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientVisitTabComponent } from './client-visit-tab.component';

describe('ClientVisitTabComponent', () => {
  let component: ClientVisitTabComponent;
  let fixture: ComponentFixture<ClientVisitTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientVisitTabComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientVisitTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
