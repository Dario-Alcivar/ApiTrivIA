using ApiTrivIA.Models;
using ApiTrivIA.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTrivIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.Usuario) || string.IsNullOrEmpty(login.Contrasenia))
            {
                return BadRequest("Usuario y contraseña son requeridos.");
            }

            var usuario = await _loginService.ValidarUsuario(login);

            if (usuario == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            return Ok("Login exitoso.");
        }
    }
}
