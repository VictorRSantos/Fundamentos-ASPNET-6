using Blog.Services;
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

        [HttpPost("v1/login")]
        public IActionResult Login()
        {

            // Vamos gerar um token e enviar para tela
            var token = _tokenService.GenerateToken(null);

            return Ok(token);


        }



    }
}