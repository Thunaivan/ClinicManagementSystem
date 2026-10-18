
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Doctor } from '../models/doctor';
import { PagedResult } from '../../../shared/models/paged-result';
import { CreateDoctor } from '../models/create-doctor';
import { UpdateDoctor } from '../models/update-doctor';

import { OnInit } from '@angular/core';
import { Department } from '../models/department';


@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  private readonly apiUrl = `${environment.apiUrl}/Doctor`;

  constructor(private readonly http: HttpClient) {}

  getDoctors(
    pageNumber: number,
    pageSize: number
  ): Observable<PagedResult<Doctor>> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize);

    return this.http.get<PagedResult<Doctor>>(this.apiUrl, { params });
  }

  getDoctorById(id: string): Observable<Doctor> {
    return this.http.get<Doctor>(`${this.apiUrl}/${id}`);
  }

  deleteDoctor(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  createDoctor(doctor: CreateDoctor): Observable<void> {
    debugger
  return this.http.post<void>(this.apiUrl, doctor);
}

updateDoctor(doctor: UpdateDoctor): Observable<void> {
  return this.http.put<void>(
    `${this.apiUrl}/${doctor.id}`,
    doctor
  );
}
}