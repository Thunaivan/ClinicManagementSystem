
using Clinic.Application.Common.Pagination;
using Clinic.Application.Doctors;
using Clinic.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationParameters parameters)
    {
        var result = await _doctorService.GetAllAsync(parameters);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);

        if (doctor is null)
        {
            return NotFound();
        }

        return Ok(doctor);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateDoctorDto dto)
    {
        var doctorId = await _doctorService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = doctorId },
            new { id = doctorId });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
    Guid id,
    UpdateDoctorDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("Route ID and DTO ID do not match.");
        }

        var doctor = await _doctorService.GetByIdAsync(id);

        if (doctor is null)
        {
            return NotFound();
        }

        await _doctorService.UpdateAsync(dto);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);

        if (doctor is null)
        {
            return NotFound();
        }

        await _doctorService.DeleteAsync(id);

        return NoContent();
    }


}