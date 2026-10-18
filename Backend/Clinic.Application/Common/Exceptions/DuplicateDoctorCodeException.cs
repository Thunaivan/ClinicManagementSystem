namespace Clinic.Application.Common.Exceptions;

public class DuplicateDoctorCodeException : Exception
{
    public DuplicateDoctorCodeException()
        : base("Duplicate doctor code.")
    {
    }
}