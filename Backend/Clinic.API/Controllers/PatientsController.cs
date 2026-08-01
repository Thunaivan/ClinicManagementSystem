using Clinic.Application.Common.Pagination;
using Clinic.Application.Interfaces;
using Clinic.Application.Patients;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;


    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
    {
        var id = await _patientService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            new { id });
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
    [FromQuery] PaginationParameters parameters)
    {
        var patients = await _patientService.GetAllAsync(parameters);

        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientDto>> GetById(Guid id)
    {
        var patient = await _patientService.GetByIdAsync(id);

        if (patient == null)
            return NotFound();

        return Ok(patient);
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePatientDto dto)
    {
        if (id != dto.Id)
            return BadRequest("Route id and body id do not match.");

        await _patientService.UpdateAsync(dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _patientService.DeleteAsync(id);

        return NoContent();
    }
}