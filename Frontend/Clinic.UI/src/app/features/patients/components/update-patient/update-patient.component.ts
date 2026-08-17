import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { PatientService } from '../../services/patient.service';
import { UpdatePatient } from '../../models/update-patient';
import { DateUtil } from '../../../../core/utils/date.util';

@Component({
  selector: 'app-update-patient',
  standalone: true,
  templateUrl: './update-patient.component.html',
  styleUrls: ['./update-patient.component.css'],
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
export class UpdatePatientComponent implements OnInit {
  patientForm: FormGroup;
  patientId!: string;
  isLoading = true;

  readonly genders = [
    { value: 1, display: 'Male' },
    { value: 2, display: 'Female' },
    { value: 3, display: 'Other' },
  ];

  constructor(
    private readonly fb: FormBuilder,
    private readonly patientService: PatientService,
    private readonly snackBar: MatSnackBar,
    private readonly router: Router,
    private readonly route: ActivatedRoute,
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

  ngOnInit(): void {
    this.patientId = this.route.snapshot.paramMap.get('id') ?? '';

    if (!this.patientId) {
      this.snackBar.open('Invalid patient ID.', 'Close', {
        duration: 3000,
      });

      this.router.navigate(['/patients']);
      return;
    }

    this.loadPatient();
  }

  private loadPatient(): void {
    this.patientService.getPatientById(this.patientId).subscribe({
      next: (patient) => {
        this.patientForm.patchValue({
          firstName: patient.firstName,
          lastName: patient.lastName,
          gender: patient.gender,
          dateOfBirth: new Date(patient.dateOfBirth),
          phoneNumber: patient.phoneNumber,
          email: patient.email,
          address: patient.address,
        });

        this.isLoading = false;
      },

      error: () => {
        this.isLoading = false;

        this.snackBar.open('Unable to load patient.', 'Close', {
          duration: 3000,
        });

        this.router.navigate(['/patients']);
      },
    });
  }

  onSubmit(): void {
    if (this.patientForm.invalid) {
      this.patientForm.markAllAsTouched();
      return;
    }

    const formValue = this.patientForm.value;

    const patient: UpdatePatient = {
      id: this.patientId,
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      gender: formValue.gender,
      dateOfBirth: DateUtil.toApiDate(formValue.dateOfBirth),
      phoneNumber: formValue.phoneNumber,
      email: formValue.email,
      address: formValue.address,
    };

    this.patientService.updatePatient(patient).subscribe({
      next: () => {
        this.snackBar.open('Patient updated successfully.', 'Close', {
          duration: 3000,
        });

        this.router.navigate(['/patients']);
      },

      error: () => {
        this.snackBar.open('Unable to update patient.', 'Close', {
          duration: 3000,
        });
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/patients']);
  }
}
