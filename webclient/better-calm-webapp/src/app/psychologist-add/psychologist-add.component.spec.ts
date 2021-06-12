import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PsychologistAddComponent } from './psychologist-add.component';

describe('PsychologistEditComponent', () => {
  let component: PsychologistAddComponent;
  let fixture: ComponentFixture<PsychologistAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PsychologistAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PsychologistAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
