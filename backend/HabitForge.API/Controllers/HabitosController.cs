using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using HabitForge.Application.Interfaces;
using HabitForge.Domain.Entities;
using HabitForge.Application.Features.Habitos.DTOs;

namespace HabitForge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitosController : ControllerBase {
    private readonly IHabitoRepository _repository;
    private readonly IValidator<CreateHabitoRequestDto> _validator;

    // El motor de inyección de .NET nos entrega la implementación concreta aquí
    public HabitosController(IHabitoRepository repository, IValidator<CreateHabitoRequestDto> validator) {
        _repository = repository; 
        _validator = validator;
    }

    // VERBO GET: Solicitar lectura de datos
    [HttpGet]
    public async Task<IActionResult> GetHabitos() {
        var habitos = await _repository.GetAllAsync();
        
        // Mapeo Manual: Entidad -> DTO
        var response = habitos.Select(h => new HabitoResponseDto {
            Id = h.Id,
            Nombre = h.Nombre
        });
        
        return Ok(response); // HTTP 200 OK
    }

    // VERBO POST: Solicitar creación de un recurso
    [HttpPost]
    public async Task<IActionResult> CrearHabito([FromBody]  CreateHabitoRequestDto request) {
        // 1. Ejecutar FluentValidation
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid) {
            return BadRequest(validationResult.Errors); // HTTP 400 automático con detalles
        }

        // 2. Mapeo Manual: DTO -> Entidad (Solo transferimos lo permitido)
        var nuevoHabito = new HabitoGlobal {
            Nombre = request.Nombre
        };

        // 3. Persistencia
        var habitoCreado = await _repository.AddAsync(nuevoHabito);
        
        // 4. Mapeo de Retorno
        var response = new HabitoResponseDto {
            Id = habitoCreado.Id,
            Nombre = habitoCreado.Nombre
        };

        return CreatedAtAction(nameof(GetHabitos), new { id = response.Id }, response);
    }
}