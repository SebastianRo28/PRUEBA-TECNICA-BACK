using prueba.Model.Modelos;
using Microsoft.EntityFrameworkCore;
using prueba.Service.Service.interfaz;
using prueba.Service.Service.Implementacion;
using prueba.repository.interfaz;
using prueba.repository.implementacion;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var SqlConnection = builder.Configuration.GetConnectionString("ToDoListConnection");

builder.Services.AddDbContext<pruebaTecnicaDbContext>(options =>
    options.UseSqlServer(SqlConnection));
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        app.UseSwagger(options =>
        {
            options.SerializeAsV2 = true;
        });
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
