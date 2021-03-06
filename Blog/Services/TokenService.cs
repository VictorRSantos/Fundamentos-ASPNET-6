using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Extensions;
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

            //Usar RoleClaimExtension
            var claims = user.GetClaims();

            //3°
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Subject(Assunto), dentro subject podemos colocar um objeto do tipo ClaimIdentity(Claims - Afirmação)
                Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8), //Tempo de duração do Token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature), //Server para desencriptar |encriptar a chave e gerar token

            };

        //4°
        var token = tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);// 5° Converte para uma string



        }
}
}