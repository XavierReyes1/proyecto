using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using YourCloset.Application.Interfaces;
using YourCloset.Domain.Entities;
using YourCloset.Application.Features.Prendas.DTOs;

namespace YourCloset.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrendasController : ControllerBase {
    private readonly IPrendaRepository _repository;
    private readonly IValidator<CreatePrendaRequestDto> _createValidator;
    private readonly IValidator<UpdatePrendaRequestDto> _updateValidator;

    public PrendasController(
        IPrendaRepository repository,
        IValidator<CreatePrendaRequestDto> createValidator,
        IValidator<UpdatePrendaRequestDto> updateValidator) 
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPrendas() {
        var prendas = await _repository.GetAllAsync();
        var response = prendas.Select(p => new PrendaResponseDto {
            Id = p.Id, Nombre = p.Nombre, Categoria = p.Categoria, Precio = p.Precio, Stock = p.Stock
        });
        return Ok(response);
    }

    // 1. GET POR ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPrendaPorId(int id) {
        var prenda = await _repository.GetByIdAsync(id);
        if (prenda == null) return NotFound($"La prenda con ID {id} no existe.");

        var response = new PrendaResponseDto {
            Id = prenda.Id, Nombre = prenda.Nombre, Categoria = prenda.Categoria, 
            Precio = prenda.Precio, Stock = prenda.Stock
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CrearPrenda([FromBody] CreatePrendaRequestDto request) {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var nuevaPrenda = new Prenda {
            Nombre = request.Nombre, Categoria = request.Categoria, Talla = request.Talla, 
            Color = request.Color, Precio = request.Precio, Stock = request.Stock
        };

        var prendaCreada = await _repository.AddAsync(nuevaPrenda);
        var response = new PrendaResponseDto {
            Id = prendaCreada.Id, Nombre = prendaCreada.Nombre, Categoria = prendaCreada.Categoria, 
            Precio = prendaCreada.Precio, Stock = prendaCreada.Stock
        };
        return CreatedAtAction(nameof(GetPrendaPorId), new { id = response.Id }, response);
    }

    // 2. PUT (ACTUALIZAR)
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarPrenda(int id, [FromBody] UpdatePrendaRequestDto request) {
        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var prendaExistente = await _repository.GetByIdAsync(id);
        if (prendaExistente == null) return NotFound($"La prenda con ID {id} no existe.");

        prendaExistente.Nombre = request.Nombre;
        prendaExistente.Categoria = request.Categoria;
        prendaExistente.Talla = request.Talla;
        prendaExistente.Color = request.Color;
        prendaExistente.Precio = request.Precio;
        prendaExistente.Stock = request.Stock;

        await _repository.UpdateAsync(prendaExistente);
        return NoContent();
    }

    // 3. DELETE (ELIMINAR)
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarPrenda(int id) {
        var eliminado = await _repository.DeleteAsync(id);
        if (!eliminado) return NotFound($"La prenda con ID {id} no existe.");
        return NoContent();
    }
}