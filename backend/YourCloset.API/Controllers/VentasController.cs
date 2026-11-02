using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using YourCloset.Application.Interfaces;
using YourCloset.Domain.Entities;
using YourCloset.Application.Features.Ventas.DTOs;

namespace YourCloset.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase {
    private readonly IVentaRepository _repository;
    private readonly IValidator<CreateVentaRequestDto> _createValidator;
    private readonly IValidator<UpdateVentaRequestDto> _updateValidator;

    public VentasController(
        IVentaRepository repository,
        IValidator<CreateVentaRequestDto> createValidator,
        IValidator<UpdateVentaRequestDto> updateValidator) 
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetVentas() {
        var ventas = await _repository.GetAllAsync();
        var response = ventas.Select(v => new VentaResponseDto {
            Id = v.Id, ClienteId = v.ClienteId, NombreCliente = $"{v.Cliente.Nombre} {v.Cliente.Apellido}",
            PrendaId = v.PrendaId, NombrePrenda = v.Prenda.Nombre, Cantidad = v.Cantidad, 
            Total = v.Total, FechaVenta = v.FechaVenta
        });
        return Ok(response);
    }

    // 1. GET POR ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVentaPorId(int id) {
        var venta = await _repository.GetByIdAsync(id);
        if (venta == null) return NotFound($"La venta con ID {id} no existe.");

        var response = new VentaResponseDto {
            Id = venta.Id, ClienteId = venta.ClienteId, NombreCliente = $"{venta.Cliente.Nombre} {venta.Cliente.Apellido}",
            PrendaId = venta.PrendaId, NombrePrenda = venta.Prenda.Nombre, Cantidad = venta.Cantidad, 
            Total = venta.Total, FechaVenta = venta.FechaVenta
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CrearVenta([FromBody] CreateVentaRequestDto request) {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var nuevaVenta = new Venta {
            ClienteId = request.ClienteId, PrendaId = request.PrendaId, 
            Cantidad = request.Cantidad, Total = request.Total
        };

        var ventaCreada = await _repository.AddAsync(nuevaVenta);
        var ventaConRelaciones = await _repository.GetByIdAsync(ventaCreada.Id);

        var response = new VentaResponseDto {
            Id = ventaConRelaciones!.Id, ClienteId = ventaConRelaciones.ClienteId, 
            NombreCliente = $"{ventaConRelaciones.Cliente.Nombre} {ventaConRelaciones.Cliente.Apellido}",
            PrendaId = ventaConRelaciones.PrendaId, NombrePrenda = ventaConRelaciones.Prenda.Nombre, 
            Cantidad = ventaConRelaciones.Cantidad, Total = ventaConRelaciones.Total, FechaVenta = ventaConRelaciones.FechaVenta
        };
        return CreatedAtAction(nameof(GetVentaPorId), new { id = response.Id }, response);
    }

    // 2. PUT (ACTUALIZAR)
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarVenta(int id, [FromBody] UpdateVentaRequestDto request) {
        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var ventaExistente = await _repository.GetByIdAsync(id);
        if (ventaExistente == null) return NotFound($"La venta con ID {id} no existe.");

        ventaExistente.ClienteId = request.ClienteId;
        ventaExistente.PrendaId = request.PrendaId;
        ventaExistente.Cantidad = request.Cantidad;
        ventaExistente.Total = request.Total;

        await _repository.UpdateAsync(ventaExistente);
        return NoContent();
    }

    // 3. DELETE (ELIMINAR)
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarVenta(int id) {
        var eliminado = await _repository.DeleteAsync(id);
        if (!eliminado) return NotFound($"La venta con ID {id} no existe.");
        return NoContent();
    }
}