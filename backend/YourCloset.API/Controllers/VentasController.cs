using Microsoft.AspNetCore.Mvc;
using YourCloset.Application.Interfaces;
using YourCloset.Domain.Entities;

namespace YourCloset.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase {
    private readonly IVentaRepository _repository;

    public VentasController(IVentaRepository repository) {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetVentas() {
        var ventas = await _repository.GetAllAsync();
        return Ok(ventas);
    }

    [HttpPost]
    public async Task<IActionResult> CrearVenta([FromBody] Venta venta) {
        if (venta.Cantidad <= 0) {
            return BadRequest("La cantidad debe ser mayor a cero.");
        }

        var nuevaVenta = await _repository.AddAsync(venta);
        return CreatedAtAction(nameof(GetVentas), new { id = nuevaVenta.Id }, nuevaVenta);
    }
}