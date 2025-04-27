using AArkhipenko.Core;
using AArkhipenko.Keycloak;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Добавление работы с Keycloak
builder.Services.AddKeycloakAuth(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
// Использование прослойки, которая добавляет ИД запроса в заголоки запроса
app.UseRequestChainMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
