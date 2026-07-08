using Clinic.Application.Interfaces;
using Clinic.Application.Patients;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _service;

    public PatientsController(IPatientService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePatientDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var patient = await _service.GetByIdAsync(id);
        if (patient == null) return NotFound();
        return Ok(patient);
    }

    [HttpPut("{id}")]
public async Task<IActionResult> Update(Guid id, UpdatePatientDto dto)
{
    if (id != dto.Id)
        return BadRequest("Route id and body id do not match.");

    await _service.UpdateAsync(dto);

    return NoContent();
}

[HttpDelete("{id}")]
public async Task<IActionResult> Delete(Guid id)
{
    await _service.DeleteAsync(id);

    return NoContent();
}
}