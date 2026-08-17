import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { PatientService } from '../../services/patient.service';
import { Patient } from '../../models/patient';
import { Router } from '@angular/router';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
@Component({
  selector: 'app-patient-list',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatTableModule,
    MatCardModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
     MatPaginatorModule
  ],
  templateUrl: './patient-list.component.html',
  styleUrl: './patient-list.component.css',
})
export class PatientListComponent implements OnInit {
  patients: Patient[] = [];
  displayedColumns = [
'patient',
'gender',
'phone',
'actions'
];
  totalRecords = 0;
  pageNumber = 1;
  pageSize = 10;
  constructor(
    private readonly patientService: PatientService,
    private router: Router,
  ) {}
  ngOnInit(): void {
    this.loadPatients();
  }
 loadPatients(): void {
  this.patientService.getPatients(this.pageNumber, this.pageSize)
    .subscribe({
      next: (result) => {
          console.log("API Result:", result);
       this.patients = result.data;
  this.totalRecords = result.totalRecords;
  this.pageNumber = result.pageNumber;
  this.pageSize = result.pageSize;
         console.log("Total Records:", this.totalRecords);
      },

      error: (err) => console.error(err),
    });
}
   //  Add the helper method here
  getGender(gender: number): string {
    switch (gender) {
      case 1:
        return 'Male';
      case 2:
        return 'Female';
      case 3:
        return 'Other';
      default:
        return 'Unknown';
    }
  }
  addPatient(): void {
  this.router.navigate(['/patients/create']);
}
  editPatient(patient: Patient) {
     this.router.navigate(['/patients/edit', patient.id]);
  }

  deletePatient(patient: Patient) {}

  onPageChange(event: PageEvent): void {
  this.pageNumber = event.pageIndex + 1;
  this.pageSize = event.pageSize;
  this.loadPatients();
}
}
