import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { Patient } from '../models/patient';
import { PagedResult } from '../../../shared/models/paged-result';
@Injectable({
  providedIn: 'root',
})
export class PatientService {
  private apiUrl = `${environment.apiUrl}/Patients`;

  constructor(private http: HttpClient) {}
  getPatients(pageNumber: number, pageSize: number): Observable<PagedResult<Patient>> {
    const params = new HttpParams().set('pageNumber', pageNumber).set('pageSize', pageSize);

    return this.http.get<PagedResult<Patient>>(this.apiUrl, { params });
  }
}
