import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContentDetailVideoComponent } from './content-detail-video.component';

describe('ContentDetailVideoComponent', () => {
  let component: ContentDetailVideoComponent;
  let fixture: ComponentFixture<ContentDetailVideoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContentDetailVideoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContentDetailVideoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
