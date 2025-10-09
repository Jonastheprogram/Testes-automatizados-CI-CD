using Api.Esg.Fiap.Data.Contexts;
using Api.Esg.Fiap.Data.Repository;
using Api.Esg.Fiap.Models;
using Api.Esg.Fiap.Services;
using Api.Esg.Fiap.ViewModel;

using AutoMapper;

using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region INICIALIZANDO O BANCO DE DADOS
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);
#endregion

#region Repositorios
builder.Services.AddScoped<IColetaRepository, ColetaRepository>();
builder.Services.AddScoped<IResiduoRepository, ResiduoRepository>();
#endregion

#region Services
builder.Services.AddScoped<IColetaService, ColetaService>();
builder.Services.AddScoped<IResiduoService, ResiduoService>();
#endregion

#region AutoMapper
// Configuração do AutoMapper
var mapperConfig = new MapperConfiguration(c => {
    c.AllowNullCollections = true;
    c.AllowNullDestinationValues = true;

    c.CreateMap<ColetaModel, ColetaViewModel>();
    c.CreateMap<ResiduoModel, ResiduoViewModel>();

    c.CreateMap<ColetaViewModel, ColetaModel>();
    c.CreateMap<ColetaCreateViewModel, ColetaModel>();
    //c.CreateMap<ColetaUpdateViewModel, ColetaModel>();

    c.CreateMap<ResiduoViewModel, ResiduoModel>();
    c.CreateMap<ResiduoCreateViewModel, ResiduoModel>();
});

// Cria o mapper
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }











