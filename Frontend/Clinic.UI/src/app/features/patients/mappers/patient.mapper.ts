import { DateUtil } from '../../../core/utils/date.util';
import { CreatePatient } from '../models/create-patient';
import { PatientFormModel } from '../models/patient-form.model';

export class PatientMapper {

  static toCreatePatient(form: PatientFormModel): CreatePatient {
    return {
      firstName: form.firstName,
      lastName: form.lastName,
      gender: form.gender,
      dateOfBirth: DateUtil.toApiDate(form.dateOfBirth),
      phoneNumber: form.phoneNumber,
      email: form.email,
      address: form.address
    };
  }
}