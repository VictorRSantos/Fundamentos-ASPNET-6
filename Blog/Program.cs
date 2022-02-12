using Blog.Data;
using Blog.Services;

var builder = WebApplication.CreateBuilder(args);

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
app.MapControllers();
app.Run();
