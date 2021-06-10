import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContentDetailAudioComponent } from './content-detail-audio.component';

describe('ContentDetailAudioComponent', () => {
  let component: ContentDetailAudioComponent;
  let fixture: ComponentFixture<ContentDetailAudioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContentDetailAudioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContentDetailAudioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
