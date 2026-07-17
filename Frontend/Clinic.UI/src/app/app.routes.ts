import { Routes } from '@angular/router';
import { PatientList } from './features/patients/components/patient-list/patient-list';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'patients',
    pathMatch: 'full'
  },
  {
    path: 'patients',
    component: PatientList
  }
];