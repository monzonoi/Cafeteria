using Cafeteria.Application.Service;
using Cafeteria.Domain;
//using Cafeteria.Infrastructure;
//using Cafeteria.Infrastructure.Repository;
using Cafeteria.Intraestructura;
using Cafeteria.Intraestructura.Repository;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Domain;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Configuración de UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

// Configuración de los repositorios
builder.Services.AddScoped<IComandaRepository, ComandaRepository>();
builder.Services.AddScoped<IMateriaPrimaRepository, MateriaPrimaRepository>();
// Agrega otros repositorios aquí

// Otras configuraciones de servicios
builder.Services.AddScoped<ICafeRepository, CafeRepository>();
builder.Services.AddScoped<ICafeService, CafeService>();




// Configuración de los servicios de aplicación
builder.Services.AddScoped<IComandaService, ComandaService>();
builder.Services.AddScoped<IMateriaPrimaService, MateriaPrimaService>();
// Agrega otros servicios de aplicación aquí

// Configuración de AutoMapper si lo estás utilizando para mapear entidades a DTOs

// Configuración de otros servicios y middleware de tu aplicación


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseHsts();


app.UseMiddleware<ErrorHandlingMiddleware>(); 


//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
