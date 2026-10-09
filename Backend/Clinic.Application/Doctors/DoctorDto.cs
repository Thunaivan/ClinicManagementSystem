
namespace Clinic.Application.Doctors;

public class DoctorDto
{
    public Guid Id { get; set; }

    public string DoctorCode { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Specialization { get; set; } = string.Empty;

    public string RegistrationNumber { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int ExperienceYears { get; set; }

    public List<DoctorDepartmentDto> Departments { get; set; } = [];
}