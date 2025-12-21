using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopularSeguros.API.Data;
using PopularSeguros.API.DTOs;
using PopularSeguros.API.Utilities; // Importante para usar la encriptación

namespace PopularSeguros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            // 1. Encriptamos la contraseña que viene del front
            string passHash = Encrypt.GetSHA256(loginDto.Password);

            // 2. Buscamos usuario que coincida en Nombre Y Contraseña cifrada
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.LoginUsuario == loginDto.Usuario && u.PasswordHash == passHash);

            // 3. Validación
            if (usuario == null)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

            // 4. Retornamos éxito y datos básicos (sin el password)
            // NOTA: En un entorno productivo real aquí generaríamos un JWT Token.
            // Para esta prueba, devolver el objeto usuario es suficiente para que React sepa que entró.
            return Ok(new
            {
                success = true,
                message = "Login exitoso",
                data = new
                {
                    id = usuario.IdUsuario,
                    nombre = usuario.NombreCompleto,
                    usuario = usuario.LoginUsuario
                }
            });
        }
    }
}