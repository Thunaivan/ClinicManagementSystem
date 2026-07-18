import { Routes } from '@angular/router';
import { PatientListComponent } from './features/patients/components/patient-list/patient-list.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'patients',
    pathMatch: 'full',
  },
  {
    path: 'patients',
    component: PatientListComponent,
  },
];
