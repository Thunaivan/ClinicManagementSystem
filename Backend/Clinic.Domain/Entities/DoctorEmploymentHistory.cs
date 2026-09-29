using Clinic.Domain.Common;

namespace Clinic.Domain.Entities;

public class DoctorEmploymentHistory : BaseEntity
{
    public Guid DoctorId { get; set; }

    public Doctor Doctor { get; set; } = null!;

    public DateTime JoinDate { get; set; }

    public DateTime? LeaveDate { get; set; }

    public string? LeaveReason { get; set; }
}