using Clinic.Application.Patients;

namespace Clinic.Application.Interfaces;

public interface IPatientService
{
    Task<Guid> CreateAsync(CreatePatientDto dto);
    Task<List<PatientDto>> GetAllAsync();
    Task<PatientDto?> GetByIdAsync(Guid id);

    Task UpdateAsync(UpdatePatientDto dto);

    Task DeleteAsync(Guid id);
}