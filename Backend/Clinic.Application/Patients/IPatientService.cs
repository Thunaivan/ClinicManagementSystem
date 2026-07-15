using Clinic.Application.Common.Pagination;
using Clinic.Application.Patients;

namespace Clinic.Application.Interfaces;

public interface IPatientService
{
    Task<Guid> CreateAsync(CreatePatientDto dto);
   //ask<List<PatientDto>> GetAllAsync();
   Task<PagedResult<PatientDto>> GetAllAsync(
    PaginationParameters parameters);
    Task<PatientDto?> GetByIdAsync(Guid id);

    Task UpdateAsync(UpdatePatientDto dto);

    Task DeleteAsync(Guid id);
}