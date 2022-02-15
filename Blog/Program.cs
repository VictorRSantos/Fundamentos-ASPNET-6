using System.Text;
using Blog;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

#region  Configuração JWT
ConfigureAuthentication(builder);

#endregion


ConfigureMvc(builder);

ConfigureService(builder);



var app = builder.Build();

LoadConfiguration(app);

app.UseAuthentication();// Primeiro Autenticação
app.UseAuthorization();// Segundo Autorização
app.UseStaticFiles();
app.MapControllers();
app.Run();

void LoadConfiguration(WebApplication app)
{
    Configuration.JwtKey = app.Configuration.GetValue<string>("JwtKey");
    Configuration.ApiKeyName = app.Configuration.GetValue<string>("ApiKeyName");
    Configuration.ApiKey = app.Configuration.GetValue<string>("ApiKey");

    var smtp = new Configuration.SmtpConfiguration();
    app.Configuration.GetSection("Smtp").Bind(smtp);
    Configuration.Smtp = smtp;

}

void ConfigureAuthentication(WebApplicationBuilder builder)
{

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

}

void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });
}

void ConfigureService(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<BlogDataContext>();
    builder.Services.AddTransient<TokenService>(); // Sempre vai criar uma nova instancia
    builder.Services.AddTransient<EmailService>();
    // builder.Services.AddScoped(); // vai durar somente durante a transação
    // builder.Services.AddSingleton(); // Só carrega uma vez na memoria



}