
using Clinic.Application.Departments;

namespace Clinic.Application.Interfaces;
public interface IDepartmentService
{
    Task<IReadOnlyList<DepartmentDto>> GetAllAsync();
}