using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using YourCloset.Application.Interfaces;
using YourCloset.Domain.Entities;
using YourCloset.Application.Features.Clientes.DTOs;

namespace YourCloset.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase {
    private readonly IClienteRepository _repository;
    private readonly IValidator<CreateClienteRequestDto> _createValidator;
    private readonly IValidator<UpdateClienteRequestDto> _updateValidator;

    public ClientesController(
        IClienteRepository repository,
        IValidator<CreateClienteRequestDto> createValidator,
        IValidator<UpdateClienteRequestDto> updateValidator) 
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetClientes() {
        var clientes = await _repository.GetAllAsync();
        var response = clientes.Select(c => new ClienteResponseDto {
            Id = c.Id, Nombre = c.Nombre, Apellido = c.Apellido, Email = c.Email
        });
        return Ok(response);
    }

    // 1. GET POR ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientePorId(int id) {
        var cliente = await _repository.GetByIdAsync(id);
        
        if (cliente == null) {
            return NotFound($"El cliente con ID {id} no existe."); // HTTP 404
        }

        var response = new ClienteResponseDto {
            Id = cliente.Id, Nombre = cliente.Nombre, 
            Apellido = cliente.Apellido, Email = cliente.Email
        };
        return Ok(response); // HTTP 200
    }

    [HttpPost]
    public async Task<IActionResult> CrearCliente([FromBody] CreateClienteRequestDto request) {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var nuevoCliente = new Cliente {
            Nombre = request.Nombre, Apellido = request.Apellido, 
            Email = request.Email, Telefono = request.Telefono, Direccion = request.Direccion
        };

        var clienteCreado = await _repository.AddAsync(nuevoCliente);
        var response = new ClienteResponseDto {
            Id = clienteCreado.Id, Nombre = clienteCreado.Nombre, 
            Apellido = clienteCreado.Apellido, Email = clienteCreado.Email
        };
        return CreatedAtAction(nameof(GetClientePorId), new { id = response.Id }, response);
    }

    // 2. PUT (ACTUALIZAR)
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarCliente(int id, [FromBody] UpdateClienteRequestDto request) {
        // Validar DTO de entrada
        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        // Buscar si existe
        var clienteExistente = await _repository.GetByIdAsync(id);
        if (clienteExistente == null) {
            return NotFound($"El cliente con ID {id} no existe."); // HTTP 404
        }

        // Mapeo Manual: DTO -> Entidad existente
        clienteExistente.Nombre = request.Nombre;
        clienteExistente.Apellido = request.Apellido;
        clienteExistente.Email = request.Email;
        clienteExistente.Telefono = request.Telefono;
        clienteExistente.Direccion = request.Direccion;

        await _repository.UpdateAsync(clienteExistente);
        
        return NoContent(); // HTTP 204 (Operación exitosa sin devolver cuerpo)
    }

    // 3. DELETE (ELIMINAR)
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarCliente(int id) {
        var eliminado = await _repository.DeleteAsync(id);
        
        if (!eliminado) {
            return NotFound($"El cliente con ID {id} no existe."); // HTTP 404
        }

        return NoContent(); // HTTP 204
    }
}