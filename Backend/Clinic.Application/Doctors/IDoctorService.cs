using Clinic.Application.Common.Pagination;
using Clinic.Application.Doctors;

namespace Clinic.Application.Interfaces;

public interface IDoctorService
{
    Task<Guid> CreateAsync(CreateDoctorDto dto);

    Task<PagedResult<DoctorDto>> GetAllAsync(
        PaginationParameters parameters);

    Task<DoctorDto?> GetByIdAsync(Guid id);

    Task UpdateAsync(UpdateDoctorDto dto);

    Task DeleteAsync(Guid id);
}