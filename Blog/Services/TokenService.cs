using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Blog.Models;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Services
{
    public class TokenService
    {
        // Essa classe é responsável por gerar o Token
        public string GenerateToken(User user)
        {

            //Dentro da classe User já existe uma lista de Roles
            // Vamos precisar instalar dois pacotes 

            /*
                dotnet add package Microsoft.AspNetCore.Authentication
                dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

            */


            //1°
            var tokenHandler = new JwtSecurityTokenHandler();

            //2° 
            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

            //3°
            var tokenDescriptor = new SecurityTokenDescriptor();

            //4°
            var token = tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);// 5° Converte para uma string



        }
    }
}