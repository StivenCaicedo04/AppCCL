using AppCCL.Data;
using AppCCL.Interfaces;
using AppCCL.Repository;
using AppCCL.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

builder.Services.AddScoped<IMovimientoInventarioRepository, MovimientoInventarioRepository>();

builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddScoped<IMovimientoInventarioService, MovimientoInventarioService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();