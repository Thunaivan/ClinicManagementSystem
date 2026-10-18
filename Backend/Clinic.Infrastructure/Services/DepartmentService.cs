
using Clinic.Application.Departments;
using Clinic.Application.Interfaces;
using Clinic.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Services;

public class DepartmentService : IDepartmentService
{
    private readonly ClinicDbContext _context;

    public DepartmentService(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<DepartmentDto>> GetAllAsync()
    {
        return await _context.Departments
            .AsNoTracking()
            .OrderBy(d => d.Name)
            .Select(d => new DepartmentDto
            {
                DepartmentId = d.Id,
                DepartmentName = d.Name
            })
            .ToListAsync();
    }
}
