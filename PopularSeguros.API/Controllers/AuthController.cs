using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopularSeguros.API.Data;
using PopularSeguros.API.DTOs;
using PopularSeguros.API.Utilities;

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

        /// <summary>
        /// Autentica un usuario en el sistema mediante credenciales.
        /// </summary>
        /// <param name="loginDto">Credenciales de acceso</param>
        /// <returns>Información básica del usuario autenticado</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            string passHash = Encrypt.GetSHA256(loginDto.Password);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.LoginUsuario == loginDto.Usuario && u.PasswordHash == passHash);

            if (usuario == null)
            {
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }

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