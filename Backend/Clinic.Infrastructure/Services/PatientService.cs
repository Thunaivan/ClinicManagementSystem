using Clinic.Application.Interfaces;
using Clinic.Application.Patients;
using Clinic.Domain.Entities;
using Clinic.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Clinic.Application.Common.Exceptions;
using Clinic.Application.Common.Pagination;


namespace Clinic.Infrastructure.Services;

public class PatientService : IPatientService
{
    private readonly ClinicDbContext _context;

    public PatientService(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(CreatePatientDto dto)
    {
        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Address = dto.Address
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return patient.Id;
    }



    public async Task<PagedResult<PatientDto>> GetAllAsync(PaginationParameters parameters)
    {
        var query = _context.Patients.AsNoTracking();
        var totalRecords = await query.CountAsync();

        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var patients = await query
        .OrderBy(p => p.FirstName)
        .Skip(skip)
        .Take(parameters.PageSize)
        .Select(p => new PatientDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            DateOfBirth = p.DateOfBirth,
            Gender = p.Gender,
            PhoneNumber = p.PhoneNumber,
            Email = p.Email,
            Address = p.Address
        })
        .ToListAsync();

        return new PagedResult<PatientDto>
        {
            Data = patients,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling((double)totalRecords / parameters.PageSize)
        };
    }


    public async Task<PatientDto?> GetByIdAsync(Guid id)
    {
        return await _context.Patients
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new PatientDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                Address = p.Address
            })
            .SingleOrDefaultAsync();
    }
    public async Task UpdateAsync(UpdatePatientDto dto)
    {
        var patient = await _context.Patients.FindAsync(dto.Id);

        if (patient == null)
            throw new NotFoundException("Patient not found.");

        patient.FirstName = dto.FirstName;
        patient.LastName = dto.LastName;
        patient.DateOfBirth = dto.DateOfBirth;
        patient.Gender = dto.Gender;
        patient.PhoneNumber = dto.PhoneNumber;
        patient.Email = dto.Email;
        patient.Address = dto.Address;

        patient.ModifiedOn = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var patient = await _context.Patients.FindAsync(id);

        if (patient == null)
            throw new NotFoundException("Patient not found.");

        _context.Patients.Remove(patient);

        await _context.SaveChangesAsync();
    }
}