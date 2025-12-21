using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopularSeguros.API.Data;

namespace PopularSeguros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CatalogosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Catalogos/Tipos
        [HttpGet("Tipos")]
        public async Task<IActionResult> GetTipos()
        {
            return Ok(await _context.TipoPolizas.ToListAsync());
        }

        // GET: api/Catalogos/Coberturas
        [HttpGet("Coberturas")]
        public async Task<IActionResult> GetCoberturas()
        {
            return Ok(await _context.Coberturas.ToListAsync());
        }

        // GET: api/Catalogos/Estados
        [HttpGet("Estados")]
        public async Task<IActionResult> GetEstados()
        {
            return Ok(await _context.EstadoPolizas.ToListAsync());
        }

        // GET: api/Catalogos/Clientes?cedula=xxx
        [HttpGet("Clientes")]
        public async Task<IActionResult> BuscarCliente([FromQuery] string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            {
                return BadRequest("Debe proporcionar una cédula.");
            }

            var cliente = await _context.Clientes
                .Where(c => c.Cedula == cedula)
                .Select(c => new 
                { 
                    c.Cedula, 
                    NombreCompleto = $"{c.Nombre} {c.PrimerApellido} {c.SegundoApellido}".Trim()
                })
                .FirstOrDefaultAsync();

            if (cliente == null)
            {
                return NotFound(new { message = "Cliente no encontrado" });
            }

            return Ok(cliente);
        }
    }
}