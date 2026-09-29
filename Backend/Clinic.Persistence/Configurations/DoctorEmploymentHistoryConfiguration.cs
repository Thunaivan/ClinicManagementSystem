using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Persistence.Configurations;

public class DoctorEmploymentHistoryConfiguration
    : IEntityTypeConfiguration<DoctorEmploymentHistory>
{
    public void Configure(
        EntityTypeBuilder<DoctorEmploymentHistory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.JoinDate)
            .IsRequired();

        builder.Property(x => x.LeaveDate)
            .IsRequired(false);

        builder.Property(x => x.LeaveReason)
            .HasMaxLength(500);

        builder.HasOne(x => x.Doctor)
            .WithMany(x => x.EmploymentHistories)
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}