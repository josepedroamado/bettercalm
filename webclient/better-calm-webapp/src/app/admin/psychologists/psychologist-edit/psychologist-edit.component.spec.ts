import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PsychologistEditComponent } from './psychologist-edit.component';

describe('PsychologistEditComponent', () => {
  let component: PsychologistEditComponent;
  let fixture: ComponentFixture<PsychologistEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PsychologistEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PsychologistEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
