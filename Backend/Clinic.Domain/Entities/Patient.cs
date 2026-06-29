using Clinic.Domain.Common;
using Clinic.Domain.Enums;
using System.Collections.Generic;
namespace Clinic.Domain.Entities;

public class Patient : BaseEntity
{ 
    
public string   FirstName { get; set; } = string.Empty;
public string LastName { get; set; } = string.Empty;
public DateOnly DateOfBirth { get; set; }
public Gender  Gender { get; set; } 
public string PhoneNumber { get; set; } = string.Empty;

public string Email { get; set; } = string.Empty;

public string Address { get; set; } = string.Empty;

public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
