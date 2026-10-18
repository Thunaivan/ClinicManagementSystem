
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';

import { DoctorService } from '../../services/doctor.service';
import { Doctor } from '../../models/doctor';

import { ConfirmDialogComponent } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { CommonPanelComponent } from '../../../../shared/components/common-panel/common-panel.component';

@Component({
  selector: 'app-doctor-list',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatTableModule,
    MatCardModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatPaginatorModule,
    MatDialogModule,
    MatTooltipModule,
    ConfirmDialogComponent,
    CommonPanelComponent
  ],
  templateUrl: './doctor-list.component.html',
  styleUrl: './doctor-list.component.css',
})
export class DoctorListComponent implements OnInit {

  doctors: Doctor[] = [];

  displayedColumns = [
    'doctor',
    'specialization',
    'phone',
    'departments',
    'actions'
  ];

  totalRecords = 0;
  pageNumber = 1;
  pageSize = 10;

  constructor(
    private readonly doctorService: DoctorService,
    private readonly router: Router,
    private readonly dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadDoctors();
  }

  loadDoctors(): void {
    this.doctorService
      .getDoctors(this.pageNumber, this.pageSize)
      .subscribe({
        next: (result) => {
          this.doctors = result.data;
          this.totalRecords = result.totalRecords;
          this.pageNumber = result.pageNumber;
          this.pageSize = result.pageSize;
        },
        error: (err) => console.error(err),
      });
  }

  getDepartmentNames(doctor: Doctor): string {
    return doctor.departments
      ?.map(department => department.departmentName)
      .join(', ') ?? '';
  }

  addDoctor(): void {
    this.router.navigate(['/doctors/create']);
  }

  editDoctor(doctor: Doctor): void {
    this.router.navigate(['/doctors/edit', doctor.id]);
  }

  deleteDoctor(doctor: Doctor): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Delete Doctor',
        message: `Are you sure you want to delete ${doctor.firstName} ${doctor.lastName}?`,
        confirmText: 'Delete',
        cancelText: 'Cancel'
      }
    });

    dialogRef.afterClosed().subscribe((confirmed) => {
      if (!confirmed) {
        return;
      }

      this.doctorService.deleteDoctor(doctor.id).subscribe({
        next: () => {
          this.loadDoctors();
        },
        error: () => {
          window.alert('Unable to delete doctor.');
        }
      });
    });
  }

  onPageChange(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadDoctors();
  }
}