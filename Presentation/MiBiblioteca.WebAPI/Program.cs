using Microsoft.OpenApi.Models;
using MiBiblioteca.Application;
using MiBiblioteca.ExternalServices;
using MiBiblioteca.Identity;
using MiBiblioteca.Persistence;
using MiBiblioteca.WebAPI.Filters;
using MiBiblioteca.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Los filtros se registran aca porque son "globales": van a aplicar a
// TODAS las actions de TODOS los controllers, sin tener que poner
// [ServiceFilter(...)] en cada uno.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<LogActionFilter>();
    options.Filters.Add<ApiExceptionFilter>();
});
builder.Services.AddEndpointsApiExplorer();

// Swagger con el boton verde de "Authorize" para poder pegar el JWT
// y seguir probando los endpoints protegidos desde la UI.
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Pegar el token con el prefijo Bearer, ej: Bearer eyJhbGciOi..."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// Cada capa registra sus propias dependencias - Program.cs no necesita
// saber los detalles de como se arma un DbContext o un JwtTokenGenerator.
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddExternalServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// El middleware de timing va antes que todo lo demas para que el
// cronometro incluya tambien el tiempo de auth y de la action en si.
app.UseMiddleware<RequestTimingMiddleware>();

// El orden importa: primero autenticacion (quien sos), despues
// autorizacion (que podes hacer).
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
