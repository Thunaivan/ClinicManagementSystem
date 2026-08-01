import { Routes } from '@angular/router';
import { PatientListComponent } from './features/patients/components/patient-list/patient-list.component';
import  { DashboardComponent } from './features/dashboard/components/dashboard/dashboard.component';

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
    import('./features/patients/components/create-patient/create-patient.component')
      .then(m => m.CreatePatientComponent)
}
];
