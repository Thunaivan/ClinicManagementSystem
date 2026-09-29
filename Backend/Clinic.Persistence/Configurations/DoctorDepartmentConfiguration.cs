using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Persistence.Configurations;

public class DoctorDepartmentConfiguration
    : IEntityTypeConfiguration<DoctorDepartment>
{
    public void Configure(EntityTypeBuilder<DoctorDepartment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Doctor)
            .WithMany(x => x.DoctorDepartments)
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Department)
            .WithMany(x => x.DoctorDepartments)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new
        {
            x.DoctorId,
            x.DepartmentId
        })
        .IsUnique();
    }
}