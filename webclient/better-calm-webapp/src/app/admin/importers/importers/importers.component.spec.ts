import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportersComponent } from './importers.component';

describe('ImportersComponent', () => {
  let component: ImportersComponent;
  let fixture: ComponentFixture<ImportersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
