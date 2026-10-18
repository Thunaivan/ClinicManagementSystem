import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { Router } from '@angular/router';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { DoctorService } from '../../services/doctor.service';
import { CreateDoctor } from '../../models/create-doctor';
import { Department } from '../../models/department';
import { DepartmentService } from '../../services/department.service';
@Component({
  selector: 'app-create-doctor',
  standalone: true,
  templateUrl: './create-doctorcomponent.html',
  styleUrls: ['./create-doctorcomponent.css'],
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
export class CreateDoctorComponent implements OnInit{
  doctorForm: FormGroup;
departments: Department[] = [];
  constructor(
    private readonly fb: FormBuilder,
    private readonly doctorService: DoctorService,
    private readonly snackBar: MatSnackBar,
    private readonly router: Router,
    private readonly departmentService: DepartmentService,
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

 onSubmit(): void {
  if (this.doctorForm.invalid) {
    this.doctorForm.markAllAsTouched();
    return;
  }

  const doctor: CreateDoctor = this.doctorForm.value;

  this.doctorService.createDoctor(doctor).subscribe({
    next: () => {
      this.snackBar.open(
        'Doctor created successfully.',
        'Close',
        { duration: 3000 }
      );

      this.router.navigate(['/doctors']);
    },

   error: (err) => {
  this.snackBar.open(
    err.error?.Message ?? 'Unable to create doctor',
    'Close',
    { duration: 3000 }
  );
},
  });
}


  cancel(): void {
    this.router.navigate(['/doctors']);
  }

  ngOnInit(): void {
  this.departmentService.getDepartments().subscribe({
    next: (departments) => {
      this.departments = departments;
    },
    error: () => {
      this.snackBar.open(
        'Unable to load departments.',
        'Close',
        { duration: 3000 }
      );
    },
  });
}
}

