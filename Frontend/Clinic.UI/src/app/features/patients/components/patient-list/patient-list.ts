import { Component } from '@angular/core';
import { PatientService } from '../../services/patient.service';
import { Patient } from '../../models/patient';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-patient-list',
  standalone: true,
  imports: [CommonModule,
    MatButtonModule
  ],
  templateUrl: './patient-list.html',
  styleUrl: './patient-list.css',
})
export class PatientList {
  patients: Patient[] = [];
  totalRecords = 0;
  pageNumber = 1;
  pageSize = 10;
  constructor(private patientService: PatientService) {}

  ngOnInit(): void {
    this.patientService.getPatients(1, 10).subscribe({
      next: (result) => {
        this.patients = result.data;
        this.totalRecords = result.totalRecords;
        this.pageNumber = result.pageNumber;
        this.pageSize = result.pageSize;
      },
      error: (err) => {
        console.error(err);
      },
    });
  }
}
