using Clinic.Application.Interfaces;
using Clinic.Application.Patients;
using Clinic.Domain.Entities;
using Clinic.Persistence.Data;
using Microsoft.EntityFrameworkCore;

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
            Gender = (Domain.Enums.Gender)dto.Gender,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Address = dto.Address
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return patient.Id;
    }

    public async Task<List<PatientDto>> GetAllAsync()
    {
        return await _context.Patients
            .Select(p => new PatientDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Gender = (int)p.Gender,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                Address = p.Address
            })
            .ToListAsync();
    }

    public async Task<PatientDto?> GetByIdAsync(Guid id)
    {
        var p = await _context.Patients.FindAsync(id);

        if (p == null) return null;

        return new PatientDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            DateOfBirth = p.DateOfBirth,
            Gender = (int)p.Gender,
            PhoneNumber = p.PhoneNumber,
            Email = p.Email,
            Address = p.Address
        };
    }
    public async Task UpdateAsync(UpdatePatientDto dto)
{
    var patient = await _context.Patients.FindAsync(dto.Id);

    if (patient == null)
        throw new Exception("Patient not found.");

    patient.FirstName = dto.FirstName;
    patient.LastName = dto.LastName;
    patient.DateOfBirth = dto.DateOfBirth;
    patient.Gender = (Domain.Enums.Gender)dto.Gender;
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
        throw new KeyNotFoundException("Patient not found.");

    _context.Patients.Remove(patient);

    await _context.SaveChangesAsync();
}
}