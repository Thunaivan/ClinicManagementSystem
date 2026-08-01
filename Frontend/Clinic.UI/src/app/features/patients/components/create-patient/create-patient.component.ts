import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { PatientService } from '../../services/patient.service';
import { PatientMapper } from '../../mappers/patient.mapper';

@Component({
  selector: 'app-create-patient',
  standalone: true,
  templateUrl: './create-patient.component.html',
  styleUrls: ['./create-patient.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSnackBarModule,
  ],
})
export class CreatePatientComponent {
  patientForm: FormGroup;

 readonly genders = [
    { value: 1, display: 'Male' },
    { value: 2, display: 'Female' },
    { value: 3, display: 'Other' },
  ];

  constructor(
    private  readonly fb: FormBuilder,
    private  readonly patientService: PatientService,
    private readonly snackBar: MatSnackBar,
    private readonly  router: Router,
  ) {
    this.patientForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      phoneNumber: [''],
      email: ['', Validators.email],
      address: [''],
    });
  }

  onSubmit(): void {
    if (this.patientForm.invalid) {
      this.patientForm.markAllAsTouched();
      return;
    }

    const patient = PatientMapper.toCreatePatient(this.patientForm.value);

    this.patientService.createPatient(patient).subscribe({
      next: () => {
        this.snackBar.open('Patient created successfully.', 'Close', {
          duration: 3000,
        });

        this.router.navigate(['/patients']);
      },

      error: () => {
        this.snackBar.open('Unable to create patient.', 'Close', {
          duration: 3000,
        });
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/patients']);
  }
}
