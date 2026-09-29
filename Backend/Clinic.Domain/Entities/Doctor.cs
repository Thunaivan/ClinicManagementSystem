
using Clinic.Domain.Common;

namespace Clinic.Domain.Entities;

public class Doctor : BaseEntity
{
    public string DoctorCode { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Specialization { get; set; } = string.Empty;

    public string RegistrationNumber { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int ExperienceYears { get; set; }

     public ICollection<DoctorDepartment> DoctorDepartments { get; set; }
        = new List<DoctorDepartment>();

    public ICollection<DoctorEmploymentHistory> EmploymentHistories { get; set; }
        = new List<DoctorEmploymentHistory>();
}