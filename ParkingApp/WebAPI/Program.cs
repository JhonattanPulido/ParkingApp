// Libraries
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Database;
using Repositories.Interfaces;
using Services;
using Services.Interfaces;
using WebAPI.ErrorHandling;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

#region Filters
builder.Services.AddScoped<ActionHandling>();
builder.Services.AddScoped<ExceptionHandling>();
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(typeof(ActionHandling));
    opt.Filters.Add(typeof(ExceptionHandling));
});
#endregion

#region Dependencies injection
builder.Services.AddScoped(_ => SqlServerContext.GetConnection()); // SQL Server Database Connection
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Auto mapper
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ILogService, LogService>();
#endregion

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
