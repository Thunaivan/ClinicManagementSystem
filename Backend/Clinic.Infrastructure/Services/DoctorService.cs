using Clinic.Application.Common.Exceptions;
using Clinic.Application.Common.Pagination;
using Clinic.Application.Doctors;
using Clinic.Application.Interfaces;
using Clinic.Domain.Entities;
using Clinic.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Services;

public class DoctorService : IDoctorService
{
    private readonly ClinicDbContext _context;

    public DoctorService(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(CreateDoctorDto dto)
    {
        var doctor = new Doctor
        {
            Id = Guid.NewGuid(),
            DoctorCode = dto.DoctorCode,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Specialization = dto.Specialization,
            RegistrationNumber = dto.RegistrationNumber,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            ExperienceYears = dto.ExperienceYears
        };

        _context.Doctors.Add(doctor);

        foreach (var departmentId in dto.DepartmentIds)
        {
            _context.DoctorDepartments.Add(new DoctorDepartment
            {
                DoctorId = doctor.Id,
                DepartmentId = departmentId
            });
        }

        await _context.SaveChangesAsync();

        return doctor.Id;
    }

    public async Task<PagedResult<DoctorDto>> GetAllAsync(
        PaginationParameters parameters)
    {
        var query = _context.Doctors
            .AsNoTracking();

        var totalRecords = await query.CountAsync();

        var skip = (parameters.PageNumber - 1) * parameters.PageSize;

        var doctors = await query
            .OrderBy(d => d.FirstName)
            .Skip(skip)
            .Take(parameters.PageSize)
            .Select(d => new DoctorDto
            {
                Id = d.Id,
                DoctorCode = d.DoctorCode,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Specialization = d.Specialization,
                RegistrationNumber = d.RegistrationNumber,
                PhoneNumber = d.PhoneNumber,
                Email = d.Email,
                ExperienceYears = d.ExperienceYears,

                Departments = d.DoctorDepartments
                    .Select(dd => new DoctorDepartmentDto
                    {
                        DepartmentId = dd.DepartmentId,
                        DepartmentName = dd.Department.Name
                    })
                    .ToList()
            })
            .ToListAsync();

        return new PagedResult<DoctorDto>
        {
            Data = doctors,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize,
            TotalRecords = totalRecords,
            TotalPages =
                (int)Math.Ceiling(
                    (double)totalRecords / parameters.PageSize)
        };
    }

    public async Task<DoctorDto?> GetByIdAsync(Guid id)
    {
        return await _context.Doctors
            .AsNoTracking()
            .Where(d => d.Id == id)
            .Select(d => new DoctorDto
            {
                Id = d.Id,
                DoctorCode = d.DoctorCode,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Specialization = d.Specialization,
                RegistrationNumber = d.RegistrationNumber,
                PhoneNumber = d.PhoneNumber,
                Email = d.Email,
                ExperienceYears = d.ExperienceYears,

                Departments = d.DoctorDepartments
                    .Select(dd => new DoctorDepartmentDto
                    {
                        DepartmentId = dd.DepartmentId,
                        DepartmentName = dd.Department.Name
                    })
                    .ToList()
            })
            .SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(UpdateDoctorDto dto)
    {
        var doctor = await _context.Doctors
            .Include(d => d.DoctorDepartments)
            .SingleOrDefaultAsync(d => d.Id == dto.Id);

        if (doctor == null)
            throw new NotFoundException("Doctor not found.");

        doctor.DoctorCode = dto.DoctorCode;
        doctor.FirstName = dto.FirstName;
        doctor.LastName = dto.LastName;
        doctor.Specialization = dto.Specialization;
        doctor.RegistrationNumber = dto.RegistrationNumber;
        doctor.PhoneNumber = dto.PhoneNumber;
        doctor.Email = dto.Email;
        doctor.ExperienceYears = dto.ExperienceYears;

        doctor.ModifiedOn = DateTime.UtcNow;

        _context.DoctorDepartments.RemoveRange(
            doctor.DoctorDepartments);

        foreach (var departmentId in dto.DepartmentIds)
        {
            doctor.DoctorDepartments.Add(new DoctorDepartment
            {
                DoctorId = doctor.Id,
                DepartmentId = departmentId
            });
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var doctor = await _context.Doctors.FindAsync(id);

        if (doctor == null)
            throw new NotFoundException("Doctor not found.");

        _context.Doctors.Remove(doctor);

        await _context.SaveChangesAsync();
    }
}