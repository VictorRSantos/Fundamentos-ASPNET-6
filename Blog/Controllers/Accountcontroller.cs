using Blog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    public class Accountcontroller : ControllerBase
    {
        private readonly TokenService _tokenService;
        public Accountcontroller(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // [AllowAnonymous]//Não precisa estar logado
        [HttpPost("v1/login")]
        public IActionResult Login([FromServices] TokenService tokenService)
        {

            // Vamos gerar um token e enviar para tela
            var token = tokenService.GenerateToken(null);

            return Ok(token);


        }

        [Authorize(Roles = "user")]
        [HttpGet("v1/user")]
        public IActionResult GetUser() => Ok(User.Identity.Name);

        [Authorize(Roles = "author")]
        [HttpGet("v1/author")]
        public IActionResult GetAuthor() => Ok(User.Identity.Name);

        [Authorize(Roles = "admin")]
        [HttpGet("v1/admin")]
        public IActionResult GetAdmin() => Ok(User.Identity.Name);




    }
}