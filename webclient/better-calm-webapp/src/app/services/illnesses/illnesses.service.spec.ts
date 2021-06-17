import { TestBed } from '@angular/core/testing';

import { IllnessesService } from './illnesses.service';

describe('IllnessesService', () => {
  let service: IllnessesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IllnessesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
