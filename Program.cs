// Program.cs
using Microsoft.EntityFrameworkCore;
using PagosCESvsCDC.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models; // <-- Necesario para Swagger


var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pagos API CESvsCDC", Version = "v1" });
});

// Agregar servicios de base de datos
builder.Services.AddDbContext<ApplicationDbContextCes>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CES")), ServiceLifetime.Scoped);

builder.Services.AddDbContext<ApplicationDbContextCdc>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CDC")), ServiceLifetime.Scoped);

builder.Services.AddControllers();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pagos API CESvsCDC v1"));
}

app.MapControllers();
app.Run();