using System.ComponentModel.DataAnnotations;

namespace Clinic.Application.Doctors;

public class UpdateDoctorDto
{


    public Guid Id { get; set; }
    
    [Required]
    [StringLength(20)]
    public string DoctorCode { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(150)]
    public string Specialization { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string RegistrationNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Range(0, 60)]
    public int ExperienceYears { get; set; }

    public List<Guid> DepartmentIds { get; set; } = [];
}