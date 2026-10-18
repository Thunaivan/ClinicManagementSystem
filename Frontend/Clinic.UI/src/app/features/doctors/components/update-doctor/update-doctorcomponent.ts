import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { DoctorService } from '../../services/doctor.service';
import { UpdateDoctor } from '../../models/update-doctor';
import { Department } from '../../models/department';
import { DepartmentService } from '../../services/department.service';

@Component({
  selector: 'app-update-doctor',
  standalone: true,
  templateUrl: './update-doctorcomponent.html',
  styleUrl: './update-doctorcomponent.css',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatSnackBarModule,
  ],
})
export class UpdateDoctorComponent implements OnInit {
  doctorForm: FormGroup;
  departments: Department[] = [];
  doctorId = '';

  constructor(
    private readonly fb: FormBuilder,
    private readonly doctorService: DoctorService,
    private readonly departmentService: DepartmentService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly snackBar: MatSnackBar,
  ) {
    this.doctorForm = this.fb.group({
      doctorCode: ['', [Validators.required, Validators.maxLength(20)]],
      firstName: ['', [Validators.required, Validators.maxLength(100)]],
      lastName: ['', [Validators.required, Validators.maxLength(100)]],
      specialization: ['', [Validators.required, Validators.maxLength(150)]],
      registrationNumber: [
        '',
        [Validators.required, Validators.maxLength(50)],
      ],
      phoneNumber: ['', Validators.maxLength(20)],
      email: ['', [Validators.email, Validators.maxLength(150)]],
      experienceYears: [
        0,
        [Validators.required, Validators.min(0), Validators.max(60)],
      ],
      departmentIds: [[], Validators.required],
    });
  }

  ngOnInit(): void {
    this.doctorId = this.route.snapshot.paramMap.get('id') ?? '';

    if (!this.doctorId) {
      this.snackBar.open('Doctor ID is missing.', 'Close', {
        duration: 3000,
      });
      this.router.navigate(['/doctors']);
      return;
    }

    this.departmentService.getDepartments().subscribe({
      next: (departments) => {
        this.departments = departments;
      },
      error: () => {
        this.snackBar.open('Unable to load departments.', 'Close', {
          duration: 3000,
        });
      },
    });

    this.loadDoctor();
  }

  loadDoctor(): void {
    this.doctorService.getDoctorById(this.doctorId).subscribe({
      next: (doctor) => {
        this.doctorForm.patchValue({
          doctorCode: doctor.doctorCode,
          firstName: doctor.firstName,
          lastName: doctor.lastName,
          specialization: doctor.specialization,
          registrationNumber: doctor.registrationNumber,
          phoneNumber: doctor.phoneNumber,
          email: doctor.email,
          experienceYears: doctor.experienceYears,
          departmentIds: doctor.departments.map(
            (department) => department.departmentId,
          ),
        });
      },
      error: () => {
        this.snackBar.open('Unable to load doctor details.', 'Close', {
          duration: 3000,
        });
      },
    });
  }

  onSubmit(): void {
    if (this.doctorForm.invalid) {
      this.doctorForm.markAllAsTouched();
      return;
    }

    const doctor: UpdateDoctor = {
      id: this.doctorId,
      ...this.doctorForm.value,
    };

    this.doctorService.updateDoctor(doctor).subscribe({
      next: () => {
        this.snackBar.open('Doctor updated successfully.', 'Close', {
          duration: 3000,
        });
        this.router.navigate(['/doctors']);
      },
      error: (err) => {
        this.snackBar.open(
          err.error?.Message ?? 'Unable to update doctor.',
          'Close',
          { duration: 3000 },
        );
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/doctors']);
  }
}
