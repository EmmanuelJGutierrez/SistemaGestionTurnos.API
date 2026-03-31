using Microsoft.EntityFrameworkCore;
using SistemaGestionTurnos.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.TypeInfoResolver =
            new System.Text.Json.Serialization.Metadata.DefaultJsonTypeInfoResolver();
    });

//integracion de DbContext con Sqlite
builder.Services.AddDbContext<AppDbContext>(Options =>
    Options.UseSqlite("Data Source=turnos.db"));
// Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Usar Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();