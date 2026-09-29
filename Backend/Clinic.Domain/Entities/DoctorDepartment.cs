using Clinic.Domain.Common;

namespace Clinic.Domain.Entities;

public class DoctorDepartment : BaseEntity
{
    public Guid DoctorId { get; set; }

    public Doctor Doctor { get; set; } = null!;

    public Guid DepartmentId { get; set; }

    public Department Department { get; set; } = null!;
}