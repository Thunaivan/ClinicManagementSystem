namespace Clinic.API.Models;

public class ErrorResponse
{
    public bool Success { get; set; } = false;

    public int StatusCode { get; set; }

    public string Message { get; set; } = string.Empty;

    public string? TraceId { get; set; }
}