using Microsoft.AspNetCore.Mvc;
using YourCloset.Application.Interfaces;
using YourCloset.Domain.Entities;

namespace YourCloset.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrendasController : ControllerBase {
    private readonly IPrendaRepository _repository;

    public PrendasController(IPrendaRepository repository) {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetPrendas() {
        var prendas = await _repository.GetAllAsync();
        return Ok(prendas);
    }

    [HttpPost]
    public async Task<IActionResult> CrearPrenda([FromBody] Prenda prenda) {
        if (string.IsNullOrWhiteSpace(prenda.Nombre)) {
            return BadRequest("El nombre de la prenda es obligatorio.");
        }

        var nuevaPrenda = await _repository.AddAsync(prenda);
        return CreatedAtAction(nameof(GetPrendas), new { id = nuevaPrenda.Id }, nuevaPrenda);
    }
}