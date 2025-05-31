using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using MusicStore.Core.Db;
using MusicStore.Platform.Repositories;
using MusicStore.Platform.Repositories.Interfaces;
using MusicStore.Platform.Services;
using MusicStore.Platform.Services.Extensions;
using MusicStore.Platform.Services.Interfaces;  

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(
    builder.Configuration.GetConnectionString("DefaultConnection"))); // Dapper Connection to DataBase

//Repositories and Services


builder.Services.RegisterPlatformServices();
builder.Services.RegisterPlatformRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
