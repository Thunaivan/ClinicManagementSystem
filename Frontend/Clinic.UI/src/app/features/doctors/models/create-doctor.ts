export interface CreateDoctor {
  doctorCode: string;
  firstName: string;
  lastName: string;
  specialization: string;
  registrationNumber: string;
  phoneNumber: string;
  email: string;
  experienceYears: number;
  departmentIds: string[];
}