export interface Doctor {
  id: string;
  doctorCode: string;
  firstName: string;
  lastName: string;
  specialization: string;
  registrationNumber: string;
  phoneNumber: string;
  email: string;
  experienceYears: number;
  departments: DoctorDepartment[];
}

export interface DoctorDepartment {
  departmentId: string;
  departmentName: string;
}