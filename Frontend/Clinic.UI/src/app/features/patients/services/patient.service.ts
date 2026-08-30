import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { Patient } from '../models/patient';
import { PagedResult } from '../../../shared/models/paged-result';
import { CreatePatient } from '../models/create-patient';
import { UpdatePatient } from '../models/update-patient';
@Injectable({
  providedIn: 'root',
})
export class PatientService {
  private readonly apiUrl = `${environment.apiUrl}/Patients`;

  constructor(private readonly http: HttpClient) {}
  getPatients(pageNumber: number, pageSize: number): Observable<PagedResult<Patient>> {
   
    const params = new HttpParams().set('pageNumber', pageNumber).set('pageSize', pageSize);

    return this.http.get<PagedResult<Patient>>(this.apiUrl, { params });
  }
  createPatient(patient: CreatePatient): Observable<void> {
    return this.http.post<void>(this.apiUrl, patient);
  }

    updatePatient(patient: UpdatePatient): Observable<void> {
    return this.http.put<void>(
      `${this.apiUrl}/${patient.id}`,
      patient
    );
  }
  getPatientById(id: string): Observable<Patient> {
  return this.http.get<Patient>(`${this.apiUrl}/${id}`);
}
deletePatient(id: string): Observable<void> {
  return this.http.delete<void>(`${this.apiUrl}/${id}`);
}
}
