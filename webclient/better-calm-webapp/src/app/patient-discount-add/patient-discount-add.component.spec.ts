import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientDiscountAddComponent } from './patient-discount-add.component';

describe('PatientDiscountAddComponent', () => {
  let component: PatientDiscountAddComponent;
  let fixture: ComponentFixture<PatientDiscountAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PatientDiscountAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientDiscountAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
