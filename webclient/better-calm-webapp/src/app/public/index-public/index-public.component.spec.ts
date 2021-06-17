import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndexPublicComponent } from './index-public.component';

describe('IndexPublicComponent', () => {
  let component: IndexPublicComponent;
  let fixture: ComponentFixture<IndexPublicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndexPublicComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IndexPublicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
