using System.Text;
using Blog;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

#region  Configuração JWT
var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters // Parametro para validar o token
    {
        ValidateIssuerSigningKey = true, //Validar a chave de assinatura 
        IssuerSigningKey = new SymmetricSecurityKey(key),// valida a chave atraves da chave Simetrica
        ValidateIssuer = false,
        ValidateAudience = false
    };

});

#endregion



builder
.Services
.AddControllers()
.ConfigureApiBehaviorOptions(options =>
{

    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddDbContext<BlogDataContext>();

builder.Services.AddTransient<TokenService>(); // Sempre vai criar uma nova instancia
// builder.Services.AddScoped(); // vai durar somente durante a transação
// builder.Services.AddSingleton(); // Só carrega uma vez na memoria


var app = builder.Build();

app.UseAuthentication();// Primeiro Autenticação
app.UseAuthorization();// Segundo Autorização

app.MapControllers();
app.Run();
