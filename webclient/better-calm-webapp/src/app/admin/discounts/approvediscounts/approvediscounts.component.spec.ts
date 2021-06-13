import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApprovediscountsComponent } from './approvediscounts.component';

describe('ApprovediscountsComponent', () => {
  let component: ApprovediscountsComponent;
  let fixture: ComponentFixture<ApprovediscountsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApprovediscountsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApprovediscountsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
