import { Routes } from '@angular/router';
import { PatientListComponent } from './features/patients/components/patient-list/patient-list.component';
import { DashboardComponent } from './features/dashboard/components/dashboard/dashboard.component';
import { UpdatePatientComponent } from './features/patients/components/update-patient/update-patient.component';

export const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
  },
  {
    path: 'patients',
    component: PatientListComponent,
  },
  {
    path: 'patients/create',
    loadComponent: () =>
      import('./features/patients/components/create-patient/create-patient.component').then(
        (m) => m.CreatePatientComponent,
      ),
  },
  {
    path: 'patients/edit/:id',
    loadComponent: () =>
      import('./features/patients/components/update-patient/update-patient.component').then
    ((m)=> m.UpdatePatientComponent),
  },
];
