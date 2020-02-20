import { TestBed } from '@angular/core/testing';

import { ApiWrapperService } from './api-wrapper.service';

describe('ApiWrapperService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApiWrapperService = TestBed.get(ApiWrapperService);
    expect(service).toBeTruthy();
  });
});
