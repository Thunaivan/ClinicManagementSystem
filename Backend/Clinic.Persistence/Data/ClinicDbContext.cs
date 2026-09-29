using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Persistence.Data;

public class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options)
     : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();
     public DbSet<Doctor> Doctors => Set<Doctor>();

    public DbSet<Department> Departments => Set<Department>();

    public DbSet<DoctorDepartment> DoctorDepartments => Set<DoctorDepartment>();

    public DbSet<DoctorEmploymentHistory> DoctorEmploymentHistories
        => Set<DoctorEmploymentHistory>();

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ClinicDbContext).Assembly);
    }
}