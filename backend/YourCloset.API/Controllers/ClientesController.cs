using Microsoft.AspNetCore.Mvc;
using YourCloset.Application.Interfaces;
using YourCloset.Domain.Entities;

namespace YourCloset.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase {
    private readonly IClienteRepository _repository;

    // Inyección de Dependencias en acción
    public ClientesController(IClienteRepository repository) {
        _repository = repository;
    }

    // VERBO GET: Solicitar lectura de datos
    [HttpGet]
    public async Task<IActionResult> GetClientes() {
        var clientes = await _repository.GetAllAsync();
        return Ok(clientes); // HTTP 200 OK
    }

    // VERBO POST: Solicitar creación de un recurso
    [HttpPost]
    public async Task<IActionResult> CrearCliente([FromBody] Cliente cliente) {
        if (string.IsNullOrWhiteSpace(cliente.Nombre)) {
            return BadRequest("El nombre del cliente es obligatorio."); // HTTP 400
        }

        var nuevoCliente = await _repository.AddAsync(cliente);

        // HTTP 201 Created
        return CreatedAtAction(nameof(GetClientes), new { id = nuevoCliente.Id }, nuevoCliente);
    }
}