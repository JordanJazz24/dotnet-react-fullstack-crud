using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopularSeguros.API.Data;
using PopularSeguros.API.DTOs;
using PopularSeguros.API.Models;

namespace PopularSeguros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolizasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PolizasController(AppDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // 1. GET: api/Polizas (Listado para la Tabla)
        // ============================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolizaDto>>> GetPolizas([FromQuery] int page = 1, [FromQuery] int pageSize = 100)
        {
            var polizas = await _context.Polizas
                .Where(p => p.Eliminado == false)
                .Include(p => p.CedulaAseguradoNavigation)
                .Include(p => p.IdTipoPolizaNavigation)
                .Include(p => p.IdCoberturaNavigation)
                .Include(p => p.IdEstadoPolizaNavigation)
                .OrderByDescending(p => p.FechaInclusion)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PolizaDto
                {
                    NumeroPoliza = p.NumeroPoliza,
                    CedulaAsegurado = p.CedulaAsegurado,
                    NombreCliente = p.CedulaAseguradoNavigation.Nombre + " " + p.CedulaAseguradoNavigation.PrimerApellido,
                    IdTipoPoliza = p.IdTipoPoliza,
                    NombreTipoPoliza = p.IdTipoPolizaNavigation.Nombre,
                    IdCobertura = p.IdCobertura,
                    NombreCobertura = p.IdCoberturaNavigation.Nombre,
                    IdEstadoPoliza = p.IdEstadoPoliza,
                    NombreEstado = p.IdEstadoPolizaNavigation.Nombre,
                    MontoAsegurado = p.MontoAsegurado,
                    Prima = p.Prima,
                    FechaVencimiento = p.FechaVencimiento,
                    Aseguradora = p.Aseguradora,
                    FechaInclusion = p.FechaInclusion,
                    FechaEmision = p.FechaEmision

                })
                .ToListAsync();

            return Ok(polizas);
        }

        // ============================================================
        // 2. GET: api/Polizas/5 (Para cargar el formulario de Edición)
        // ============================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<PolizaDto>> GetPoliza(int id)
        {
            var poliza = await _context.Polizas
                .Where(p => p.Eliminado == false)
                .Include(p => p.CedulaAseguradoNavigation)
                .Include(p => p.IdTipoPolizaNavigation)
                .Include(p => p.IdCoberturaNavigation)
                .Include(p => p.IdEstadoPolizaNavigation)
                .FirstOrDefaultAsync(p => p.NumeroPoliza == id);

            if (poliza == null)
            {
                return NotFound();
            }

            // Mapeo manual simple
            var polizaDto = new PolizaDto
            {
                NumeroPoliza = poliza.NumeroPoliza,
                CedulaAsegurado = poliza.CedulaAsegurado,
                NombreCliente = poliza.CedulaAseguradoNavigation.Nombre,
                IdTipoPoliza = poliza.IdTipoPoliza,
                NombreTipoPoliza = poliza.IdTipoPolizaNavigation.Nombre,
                IdCobertura = poliza.IdCobertura,
                NombreCobertura = poliza.IdCoberturaNavigation.Nombre,
                IdEstadoPoliza = poliza.IdEstadoPoliza,
                NombreEstado = poliza.IdEstadoPolizaNavigation.Nombre,
                MontoAsegurado = poliza.MontoAsegurado,
                Prima = poliza.Prima,
                FechaVencimiento = poliza.FechaVencimiento,
                Aseguradora = poliza.Aseguradora,
                FechaInclusion = poliza.FechaInclusion,
                FechaEmision = poliza.FechaEmision        
            };

            return Ok(polizaDto);
        }

        // ============================================================
        // 3. POST: api/Polizas (Crear Nueva)
        // ============================================================
        [HttpPost]
        public async Task<ActionResult<Poliza>> PostPoliza(PolizaCreateDto polizaDto)
        {
            // Validación de fechas lógicas
            if (polizaDto.FechaVencimiento <= polizaDto.FechaEmision)
            {
                return BadRequest("La fecha de vencimiento debe ser posterior a la fecha de emisión.");
            }

            // Validamos que el cliente exista antes de guardar
            var clienteExiste = await _context.Clientes.AnyAsync(c => c.Cedula == polizaDto.CedulaAsegurado);
            if (!clienteExiste)
            {
                return BadRequest("El cliente con esa cédula no existe.");
            }

            // Mapeamos el DTO a la Entidad de BD
            var nuevaPoliza = new Poliza
            {
                CedulaAsegurado = polizaDto.CedulaAsegurado,
                IdTipoPoliza = polizaDto.IdTipoPoliza,
                IdCobertura = polizaDto.IdCobertura,
                IdEstadoPoliza = polizaDto.IdEstadoPoliza,
                MontoAsegurado = polizaDto.MontoAsegurado,
                Prima = polizaDto.Prima,
                FechaEmision = polizaDto.FechaEmision,
                FechaVencimiento = polizaDto.FechaVencimiento,
                Aseguradora = polizaDto.Aseguradora,
                FechaInclusion = DateTime.Now,
                Eliminado = false // Default
            };

            _context.Polizas.Add(nuevaPoliza);
            await _context.SaveChangesAsync(); // Requisito: Async [cite: 24]

            return CreatedAtAction("GetPoliza", new { id = nuevaPoliza.NumeroPoliza }, nuevaPoliza);
        }

        // ============================================================
        // 4. PUT: api/Polizas/5 (Editar)
        // ============================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoliza(int id, PolizaCreateDto polizaDto)
        {
            if (polizaDto.FechaVencimiento <= polizaDto.FechaEmision)
            {
                return BadRequest("La fecha de vencimiento debe ser posterior a la fecha de emisión.");
            }

            var polizaExistente = await _context.Polizas.FindAsync(id);

            if (polizaExistente == null || polizaExistente.Eliminado == true)
            {
                return NotFound();
            }

            polizaExistente.CedulaAsegurado = polizaDto.CedulaAsegurado;
            polizaExistente.IdTipoPoliza = polizaDto.IdTipoPoliza;
            polizaExistente.IdCobertura = polizaDto.IdCobertura;
            polizaExistente.IdEstadoPoliza = polizaDto.IdEstadoPoliza;
            polizaExistente.MontoAsegurado = polizaDto.MontoAsegurado;
            polizaExistente.Prima = polizaDto.Prima;
            polizaExistente.FechaEmision = polizaDto.FechaEmision;
            polizaExistente.FechaVencimiento = polizaDto.FechaVencimiento;
            polizaExistente.Aseguradora = polizaDto.Aseguradora;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Polizas.AnyAsync(p => p.NumeroPoliza == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // ============================================================
        // 5. DELETE: api/Polizas/5 (Eliminado LÓGICO)
        // ============================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoliza(int id)
        {
            var poliza = await _context.Polizas.FindAsync(id);
            if (poliza == null)
            {
                return NotFound();
            }

            // Requisito CRÍTICO: No borrar, solo marcar como eliminado 
            poliza.Eliminado = true;

            // Le decimos a EF que hubo un cambio
            _context.Entry(poliza).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}