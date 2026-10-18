import { TestBed } from '@angular/core/testing';
import {DoctorService} from './doctor.service'


describe('Service', () => {
  let service: any;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DoctorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
