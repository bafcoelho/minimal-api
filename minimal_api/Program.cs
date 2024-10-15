using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.Servicos;
using minimal_api.Entidades;
using minimal_api.Infraestrutura.Db;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DbContexto>(options => {
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});



var app = builder.Build();




app.MapGet("/", () => "Hello World!");

app.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico)=>{
    Administrador? adm = administradorServico.Login(loginDTO);

    if(adm != null)
        return Results.Ok($"Administrador {adm.Email} fez login com sucesso!");
    else
        return Results.Unauthorized();
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();