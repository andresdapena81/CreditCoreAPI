using Microsoft.EntityFrameworkCore;
using CreditCore.Infrastructure.Persistence;
using CreditCore.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// 🚀 Configurar EF Core con SQL Server
builder.Services.AddDbContext<CreditDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🚀 Inyección de Dependencias de Servicios
builder.Services.AddScoped<PaymentProjectionService>();

// 🚀 Agregar soporte para controladores y API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🚀 Habilitar CORS (Opcional - si tienes frontend separado)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// Habilitar Swagger para pruebas
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar CORS (Opcional)
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();

// 🚀 Registrar los controladores
app.MapControllers();

app.MapGet("/", () => "CreditCore API Running...");

app.Run();
