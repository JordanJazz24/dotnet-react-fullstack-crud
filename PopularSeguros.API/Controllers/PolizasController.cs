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

        /// <summary>
        /// Obtiene el listado paginado de pólizas activas con información relacionada del cliente y catálogos.
        /// </summary>
        /// <param name="page">Número de página (base 1)</param>
        /// <param name="pageSize">Cantidad de registros por página</param>
        /// <returns>Lista de pólizas en formato DTO</returns>
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

        /// <summary>
        /// Obtiene una póliza específica por su número de póliza.
        /// </summary>
        /// <param name="id">Número de póliza</param>
        /// <returns>Detalle de la póliza en formato DTO</returns>
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

        /// <summary>
        /// Crea una nueva póliza en el sistema.
        /// </summary>
        /// <param name="polizaDto">Datos de la póliza a crear</param>
        /// <returns>La póliza creada con su número asignado</returns>
        [HttpPost]
        public async Task<ActionResult<Poliza>> PostPoliza(PolizaCreateDto polizaDto)
        {
            if (polizaDto.FechaVencimiento <= polizaDto.FechaEmision)
            {
                return BadRequest("La fecha de vencimiento debe ser posterior a la fecha de emisión.");
            }

            var clienteExiste = await _context.Clientes.AnyAsync(c => c.Cedula == polizaDto.CedulaAsegurado);
            if (!clienteExiste)
            {
                return BadRequest("El cliente con esa cédula no existe.");
            }

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
                Eliminado = false
            };

            _context.Polizas.Add(nuevaPoliza);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoliza", new { id = nuevaPoliza.NumeroPoliza }, nuevaPoliza);
        }

        /// <summary>
        /// Actualiza los datos de una póliza existente.
        /// </summary>
        /// <param name="id">Número de póliza a actualizar</param>
        /// <param name="polizaDto">Nuevos datos de la póliza</param>
        /// <returns>Resultado de la operación</returns>
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

        /// <summary>
        /// Realiza un borrado lógico de una póliza marcándola como eliminada.
        /// </summary>
        /// <param name="id">Número de póliza a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoliza(int id)
        {
            var poliza = await _context.Polizas.FindAsync(id);
            if (poliza == null)
            {
                return NotFound();
            }

            poliza.Eliminado = true;
            _context.Entry(poliza).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}